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

                    data.Points.Add(new XY(p,q,n,tot,j));
                }
            }
            data.LinearRegression();
            foreach (var xy in data.Points)
            {
                var expectedSigDigitsP =(int)(data.Intercept + data.Slope * xy.P.ToString().Length);
                var expectedSigDigitsQ =(int)(data.Intercept + data.Slope * xy.Q.ToString().Length);
                var subP = BigInteger.Parse(xy.P.ToString().Substring(0, expectedSigDigitsP));
                var subQ = BigInteger.Parse(xy.Q.ToString().Substring(0, expectedSigDigitsQ));
                if (!debug) continue;
                Console.WriteLine("\nP:\n"+xy.P);
                Console.WriteLine("SubP:\n"+subP);
                Console.WriteLine("\nQ:\n"+xy.Q);
                Console.WriteLine("SubQ:\n"+subQ);
                Console.WriteLine("\nN:\n"+xy.N);
                Console.WriteLine("SubN:\n" + BigInteger.Multiply(subP,subQ));
                Console.WriteLine("\nTotient:\n"+xy.Totient);
                Console.WriteLine("SubTotient:\n" + BigInteger.Multiply(subP-1,subQ-1));
                Console.WriteLine("\nN-Totient:\n" + BigInteger.Subtract(xy.N,xy.Totient));
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

        public static BigInteger GuessTotient(Stats data, BigInteger n, BigInteger e, BigInteger realTotient)
        {
            var totient = BigInteger.MinusOne;
            var nSize = n.ToString().Length;
            var expectedSigDigits =(int)(data.Intercept + data.Slope * nSize);

            var baseNString = n.ToString().Substring(0,expectedSigDigits);
            var maxString = n.ToString().Substring(expectedSigDigits);
            var max = BigInteger.Parse(maxString);
            var min = BigInteger.Zero;
            var midpoint = BigInteger.Divide(max,2);
            var target = BigInteger.Parse(realTotient.ToString().Substring(expectedSigDigits));

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
                bi += 1;     
            }
        }
        private static IEnumerable<BigInteger> BigIntSequenceReverse(BigInteger min,BigInteger max)
        {
            var bi = max;
            while (bi>min)
            {
                yield return bi;
                bi -= 1;     
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