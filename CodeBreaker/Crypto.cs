using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CodeBreaker.Models;

namespace CodeBreaker
{
    public static class Crypto
    {
        public static double DecryptChance(string text)
        {
            var strDict = new SortedDictionary<string, int>();

            var list = text.ToUpper().Split(" ,!.?:;\"()[]{}*"
                .ToCharArray()).ToList();
            foreach (var cha in list)
            {
                var count = 1;
                if (strDict.ContainsKey(cha))
                    count = strDict[cha] + 1;
                strDict[cha] = count;
            }

            if (strDict.Count == 0) return 0;

            var wordCount = 0.00;
            foreach (var str in strDict.Keys)
            {
                var strLen = (double) str.Length;
                var amountLegible = str.ToCharArray()
                    .Select(Convert.ToInt32)
                    .Count(chaInt => chaInt <= 90 && chaInt >= 65 || chaInt == 39);
                if (amountLegible / strLen >= .95)
                    wordCount++;
            }

            var result = wordCount / strDict.Count;
            //Console.WriteLine(result);
            return result;
        }


        public static Stats CompareNWithTotient(int k, int iterations, bool debug)
        {
            var data = new Stats();
            for (var x = 0; x < iterations; x++)
            {
                for (var i = 384; i <= k; i+=8)
                {
                    var csp = new RSACryptoServiceProvider(i);
                    var parameters = csp.ExportParameters(true);
                    var p = FromBigEndian(parameters.P);
                    //StorePrime(p,i);
                    var q = FromBigEndian(parameters.Q);
                    //StorePrime(q,i);
                    var n = BigInteger.Multiply(p, q);
                    var tot = BigInteger.Multiply(p - 1, q - 1);

                    var nStr = n.ToString();
                    var totStr = tot.ToString();
                    var j = 0;
                    for (; j < nStr.Length; j++) if (nStr[j] != totStr[j]) break;

                    data.Points.Add(new XY(p,q,n,tot,i,j));
                }
            }
            data.LinearRegressionSize();

            foreach (var xy in data.Points)
            {
                var expectedSigDigits = (int)(data.SizeIntercept + data.SizeSlope * xy.X);
                var baseNString = xy.N.ToString().Substring(0, expectedSigDigits);
                var maxString = xy.N.ToString().Substring(expectedSigDigits);
                var max = BigInteger.Parse(maxString);
                var min = BigInteger.Zero;
                var midpoint = BigInteger.Divide(max, 2);
                var tot = xy.Totient.ToString().Substring(expectedSigDigits);
                var difFromMid = double.Parse(BigInteger.Subtract(BigInteger.Parse(tot), midpoint).ToString());
                var diffSplit = difFromMid.ToString().Split('E');
                xy.Diff = double.Parse(diffSplit[0]);
                xy.DiffMagnitude = int.Parse(diffSplit[1]);
            }

            foreach (var xy in data.Points)
            {
                if(xy.DiffMagnitude - 55 <= 0) continue;
                xy.Diff *= (xy.DiffMagnitude - 55);
            }

            if (!debug) return data;

            foreach (var xy in data.Points)
            {
                Console.WriteLine("\nP:\n"+xy.P);
                Console.WriteLine("\nQ:\n"+xy.Q);
                Console.WriteLine("\nN:\n"+xy.N);
                Console.WriteLine("\nTotient:\n"+xy.Totient);
            }
            return data;
        }

        public static void AttemptFactor(BigInteger n, int size)
        {
            using (var context = new PrimeContext())
            {
                foreach (var s in context.Primes.Where(p => p.Size == size))
                {
                    var p = BigInteger.Parse(s.NumberString);
                    var q = BigInteger.Divide(n, p);

                    if (context.Primes.Any(pr => pr.NumberString == q.ToString())
                        || IsPrime(q))
                    {
                        Console.WriteLine("N: "+n);
                        Console.WriteLine("P: "+p);
                        Console.WriteLine("Q: "+q);
                        break;
                    }                   
                }
            }
        }

        public static BigInteger GetN(byte[] p, byte[] q)
        {
            return BigInteger.Multiply(FromBigEndian(p),FromBigEndian(q));
        }

        public static BigInteger GuessTotient(Stats data, BigInteger n, int keySize, BigInteger e, BigInteger realTotient)
        {
            var totient = BigInteger.MinusOne;
            var expectedSigDigits =(int)(data.SizeIntercept + data.SizeSlope * keySize);

            var baseNString = n.ToString().Substring(0,expectedSigDigits);
            var maxString = n.ToString().Substring(expectedSigDigits);
            var max = BigInteger.Parse(maxString);
            var min = BigInteger.Zero;
            var midpoint = BigInteger.Divide(max,2);
            var target = BigInteger.Parse(realTotient.ToString().Substring(expectedSigDigits));
            var midpointLastDigit = midpoint.ToString()[midpoint.ToString().Length - 1];
            if(!new List<char> {'0', '2', '4', '5', '6', '8'}.Contains(midpointLastDigit))
                BigInteger.Add(midpoint,BigInteger.One);

            var source = new CancellationTokenSource();
            Parallel.Invoke(new ParallelOptions{CancellationToken = source.Token, MaxDegreeOfParallelism = Environment.ProcessorCount},
                () => Parallel.ForEach(BigIntSequence(midpoint, max), i =>
                    {
                        //Console.WriteLine("mid to max: "+i);
                        if (BigInteger.Compare(i,target) != 0) return;
                        Console.WriteLine("mid to max: " + i);
                        totient = i;
                        source.Cancel();
                    }),
                ()=> Parallel.ForEach(BigIntSequenceReverse(min, midpoint), i =>
                    {
                        //Console.WriteLine("min to mid: "+i);
                        if (BigInteger.Compare(i, target) != 0) return;
                        Console.WriteLine("mid to max: " + i);
                        totient = i;
                        source.Cancel();
                    })
            );
            return BigInteger.Parse(baseNString+totient);
        }

        private static IEnumerable<BigInteger> BigIntSequence(BigInteger min,BigInteger max)
        {
            var bi = min;
            while (bi<max)
            {
                yield return bi;
                bi += 2;     
            }
        }
        private static IEnumerable<BigInteger> BigIntSequenceReverse(BigInteger min,BigInteger max)
        {
            var bi = max;
            while (bi>min)
            {                
                yield return bi;
                bi -= 2;     
            }
        }


        public static BigInteger FromBigEndian(byte[] p)
        {
            var q = p.Reverse().ToArray();
            return new BigInteger((p[0] < 128 ? q : q.Concat(new byte[] { 0 })).ToArray());
        }

        public static BigInteger ModInverse(this BigInteger a, BigInteger n)
        {
            var i = n;
            var v = BigInteger.Zero;
            var d = BigInteger.One;
            while (a>0)
            {
                var t = i / a;
                var x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t*x;
                v = x;
            }
            v %= n;
            if (v<0) v = (v+n)%n;
            return v;
        }

        public static void StorePrime(BigInteger p, int s)
        {
            using (var context = new PrimeContext())
            {
                var prime = new Prime {NumberString = p.ToString(), Size = s};
                if (context.Primes.All(pr => pr.NumberString != prime.NumberString))
                {
                    context.Primes.Add(prime);
                    context.SaveChanges();
                }
            }
        }

        public static bool IsPrime(BigInteger num)
        {
            var numStr = num.ToString();
            var lastChar = numStr[numStr.Length - 1];
            if (new List<char>{'0','2','4','5','6','8'}.Contains(lastChar))
            {
                return false;
            }

            return false;
        }
    }
}