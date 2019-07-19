using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CodeBreaker.Models;
using Extreme.Mathematics;

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

        public static Stats CompareNWithTotient(Stats data, int k, int iterations, bool debug)
        {
            if(data == null) data = new Stats();

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
                    var xy = new XY(p, q, n, tot, i, j);

                    if (!data.Points.Exists(point => BigInteger.Compare(point.N,xy.N) == 0))
                        data.Points.Add(xy);
                }
            }
            data.LinearRegressionSize();
            var are0 = 0;
            var are1 = 0;
            var test0 = 0;
            var test1 = 0;
            var recounts = 0;
            var magnitudes = new List<double>();
            foreach (var xy in data.Points)
            {
                var expectedSigDigits = (int)Math.Round((data.SizeIntercept + data.SizeSlope * xy.X)-2.25);
                BigInteger n, tot;
                string maxString, totString;
                var count = 0;
                do
                {
                    maxString = xy.N.ToString().Substring(expectedSigDigits);
                    n = BigInteger.Parse(maxString);
                    totString = xy.Totient.ToString().Substring(expectedSigDigits);
                    tot = BigInteger.Parse(totString);
                    expectedSigDigits--;
                    count++;
                } while (BigInteger.Compare(n, tot) < 0);

                if (count > 1) recounts++;

                xy.Ratio = BigFloat.Divide(tot,n);
                xy.Diff = double.Parse(BigInteger.Subtract(n, tot).ToString());
                var diffSplit = xy.Diff.ToString(CultureInfo.InvariantCulture).Split('E');
                var diffMag = double.Parse(diffSplit[1]);
                magnitudes.Add(xy.X / diffMag);
                var differences = new List<int>();
                Console.WriteLine("  N: "+n);
                Console.WriteLine("Tot: "+tot);
                Console.WriteLine("Size: " + xy.X);
                Console.WriteLine("Diff: " + xy.Diff);
                Console.WriteLine("Size / Diff Mag: " + xy.X / diffMag);

                for (var i = 0; i < maxString.Length; i++)
                {
                    var nDigit = int.Parse(maxString[i].ToString());
                    var totDigit = int.Parse(totString[i].ToString());
                    var diff = Math.Abs(nDigit - totDigit);
                    //Console.Write(diff+", ");
                    differences.Add(diff);
                    //Console.WriteLine("N["+i+"]: " + nDigit);
                    //Console.WriteLine("Totient["+i+"]: " + totDigit);
                }
                Console.WriteLine();

                if (differences[0] == 0)
                {
                    are0++;
                    if (differences[1] <= 5) test0++;
                }
                else if (differences[0] == 1)
                {
                    are1++;
                    if (differences[1] >= 5) test1++;
                }
            }
            Console.WriteLine("Recounts: " + recounts);
            Console.WriteLine("Are 0: " + are0 + "/" + data.Points.Count);
            Console.WriteLine("Are <= 5: " + test0 + "/" + are0);
            Console.WriteLine("Are 1: " + are1 + "/" + data.Points.Count);
            Console.WriteLine("Are >= 5: " + test1 + "/" + are1);
            Console.WriteLine("Average key size / differance magnitude: " + magnitudes.Average());
            data.Points.RemoveAll(p => p.Ratio > 1);

            if (!debug) return data;

            foreach (var xy in data.Points)
            {
                Console.WriteLine("\nP:\n"+xy.P);
                Console.WriteLine("Q:\n"+xy.Q);
                Console.WriteLine("N:\n"+xy.N);
                Console.WriteLine("Totient:\n"+xy.Totient);
                Console.WriteLine("Ratio: " + xy.Ratio);
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

        public static BigInteger GuessTotient(Stats data, BigInteger n, int keySize, BigInteger e, BigInteger realTotient)
        {
            var totient = BigInteger.MinusOne;
            var expectedSigDigits = (int)Math.Round((data.SizeIntercept + data.SizeSlope * keySize) - 2.25);

            var baseNString = n.ToString().Substring(0,expectedSigDigits);
            var maxString = n.ToString().Substring(expectedSigDigits);
            var max = BigInteger.Parse(maxString);
            var min = (BigInteger)BigFloat.Round(BigFloat.Multiply(new BigFloat(max), data.AverageRatio));
            var maxDigit = int.Parse(maxString[0]+"");

            //Remove after testing
            var temp = expectedSigDigits;
            BigInteger tempN, tempTot;
            do
            {
                var str = n.ToString().Substring(temp);
                tempN = BigInteger.Parse(str);
                var totString = realTotient.ToString().Substring(temp);
                tempTot = BigInteger.Parse(totString);
                temp--;
            } while (BigInteger.Compare(tempN, tempTot) < 0);
            var nDouble = double.Parse(tempN.ToString());
            var totDouble = double.Parse(tempTot.ToString());
            Console.WriteLine("Ratio: " + totDouble / nDouble);
            var target = BigInteger.Parse(realTotient.ToString().Substring(expectedSigDigits));
            Console.WriteLine(min <= target && target <= max);

            Parallel.ForEach(BigIntSequenceReverse(min, BigInteger.Subtract(max,BigInteger.One)), (i, state) =>
            {
                //Console.WriteLine("mid to max: "+i);
                if (BigInteger.Compare(i, target) != 0) return;
                totient = i;
                state.Break();
            });
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