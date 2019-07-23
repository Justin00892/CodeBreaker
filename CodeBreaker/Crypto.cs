using System;
using System.Collections.Generic;
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

        public static Stats CompareNWithTotient(Stats data, int start, int stop, int iterations, bool debug)
        {
            if(data == null) data = new Stats();

            for (var i = start; i <= stop; i+=8)
            {
                for (var x = 0; x < iterations; x++)
                {
                    var csp = new RSACryptoServiceProvider(i);
                    var parameters = csp.ExportParameters(true);
                    var p = FromBigEndian(parameters.P);
                    //StorePrime(p,i);
                    var q = FromBigEndian(parameters.Q);
                    //StorePrime(q,i);
                    var n = BigInteger.Multiply(p, q);
                    
                    if (data.Points.Exists(point => BigInteger.Compare(point.N, n) == 0)) continue;

                    var tot = BigInteger.Multiply(p - 1, q - 1);
                    var nStr = n.ToString();
                    var totStr = tot.ToString();
                    var j = 0;
                    for (; j < nStr.Length; j++) if (nStr[j] != totStr[j]) break;
                    var xy = new XY(p, q, n, tot, i, j);
                    data.Points.Add(xy);
                }
            }
            data.LinearRegression();

            if (debug)
            {
                var diffs = new List<List<int>>();
                var diffMagCount = 0;
                var sharedCount = 0;
                foreach (var xy in data.Points)
                {
                    var expectedSigDigits = (int)Math.Round((data.Intercept + data.Slope * xy.X) - .5);
                    var maxString = xy.N.ToString().Substring(xy.Y);
                    var totString = xy.Totient.ToString().Substring(xy.Y);

                    var mag = (int)Math.Round((xy.X / data.DiffFactor) - 1);

                    var diff = new List<int>();
                    for (var i = 0; i < maxString.Length; i++)
                    {
                        var nDigit = int.Parse(maxString[i] + "");
                        var totDigit = int.Parse(totString[i] + "");
                        diff.Add(Math.Abs(nDigit - totDigit));
                    }
                    diffs.Add(diff);

                    Console.WriteLine("\nKey Size: " + xy.X);
                    Console.WriteLine("Full: ");
                    Console.WriteLine("      N: " + xy.N);
                    Console.WriteLine("Totient: " + xy.Totient);
                    Console.WriteLine("Predicted Shared Digits: " + expectedSigDigits);
                    Console.WriteLine("Actual Shared Digits: " + xy.Y);
                    Console.WriteLine("Dynamic Portion: ");
                    Console.WriteLine("  N: " + maxString);
                    Console.WriteLine("Tot: " + totString);
                    Console.WriteLine("Estimated Diff Magnitude: " + mag);
                    if (Math.Abs(mag - xy.Y) <= 1) diffMagCount++;
                    if (Math.Abs(expectedSigDigits - xy.Y) <= 1) sharedCount++;
                    Console.WriteLine("Diff: " + xy.Diff);
                }
                Console.WriteLine("\nSample Size: " + data.Points.Count);
                Console.WriteLine("First Digit Diffs:");
                for (var i = 0; i < 10; i++)
                    Console.WriteLine(i + ": " + diffs.Count(d => d[0] == i));
                Console.WriteLine("First Digit Diff < 4: Second Digit Diffs:");
                for (var i = 0; i < 10; i++)
                    Console.WriteLine(i + ": " + diffs.Count(d => d[0] < 4 && d[1] == i));

                //Console.WriteLine("Times Predicted Diff Mag = Actual Shared Digits +- 1: " + diffMagCount + "/" + data.Points.Count);
                //Console.WriteLine("Times PredictedSharedDigits = Actual Shared Digits +- 1: " + sharedCount + "/" + data.Points.Count);
            }

            data.Points.RemoveAll(p => p.Ratio > 1);
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
            var expectedSigDigits = (int)Math.Round((data.Intercept + data.Slope * keySize)-.5);

            var baseNString = n.ToString().Substring(0,expectedSigDigits);
            var maxString = n.ToString().Substring(expectedSigDigits);
            var max = BigInteger.Subtract(BigInteger.Parse(maxString), BigInteger.One);
            var mag = (int)Math.Round((keySize / data.DiffFactor) - 1);
            var minDiffString = "1";
            while (minDiffString.Length <= mag)
                minDiffString += "0";
            var minDiff = BigInteger.Parse(minDiffString);
            max = BigInteger.Subtract(max, minDiff);
            var min = BigInteger.Parse(maxString.Substring(1));


            //Remove this block after testing
            var nStr = n.ToString();
            var totStr = realTotient.ToString();
            var actualShared = 0;
            for (; actualShared < n.ToString().Length; actualShared++) if (nStr[actualShared] != totStr[actualShared]) break;
            var target = BigInteger.Parse(realTotient.ToString().Substring(expectedSigDigits));
            Console.WriteLine("\n      N: " + nStr.Substring(actualShared));
            Console.WriteLine("Totient: " + totStr.Substring(actualShared));
            Console.WriteLine("Predicted Shared: "+expectedSigDigits);
            Console.WriteLine("Actual Shared: "+actualShared);
            Console.WriteLine("Diff: " + double.Parse(BigInteger.Subtract(n,realTotient).ToString()));
            Console.WriteLine("Estimated Diff Magnitude: " + mag);
            Console.WriteLine(max >= target && target >= min);


            Parallel.ForEach(BigIntSequenceReverse(min, max), (i, state) =>
            {
                if (BigInteger.Compare(i,target) != 0) return;
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