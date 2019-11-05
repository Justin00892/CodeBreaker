using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Alea;
using Alea.Parallel;
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

        public static List<XY> CompareNWithTotient(int start, int stop, int iterations, bool debug, bool addToDatabase)
        {
            
            var points = new List<XY>();
            for (var i = start; i <= stop; i+=8)
            {
                for (var x = 0; x < iterations; x++)
                {
                    using (var csp = new RSACryptoServiceProvider(i))
                    {
                        var timestamp = DateTime.Now.ToUniversalTime();
                        var parameters = csp.ExportParameters(true);
                        var p = new BigInteger(parameters.P.FromBigEndian());
                        var q = new BigInteger(parameters.Q.FromBigEndian());
                        var n = BigInteger.Multiply(p, q);
                        var tot = BigInteger.Multiply(p - 1, q - 1);
                        var xy = new XY(p, q, n, tot, i,timestamp);

                        //if(debug)xy.PrintForms();

                        points.Add(xy);
                    }
                }
            }

            if (addToDatabase) AddToDatabase(points);

            if (debug)
            {
                var data = new Stats();
                data.AddPoints(points);
                foreach (var grouping in points.GroupBy(p => p.X))
                {
                    Console.WriteLine("\n" + grouping.First().X);
                    var results = new SortedDictionary<int, int>();
                    foreach (var point in grouping)
                    {
                        if (!results.ContainsKey(point.Y))
                            results[point.Y] = 0;
                        results[point.Y]++;
                    }
                    foreach (var result in results)
                        Console.WriteLine(result.Key + ": " + result.Value);
                    
                    Console.WriteLine(data.BytesRegression.GetPredictionInterval(grouping.First().X));
                }
            }

            return points;
        }

        public static BigInteger GuessTotient(Stats data, BigInteger n, int keySize, BigInteger e, BigInteger realTotient, bool debug)
        {
            var totient = BigInteger.MinusOne;
            var expectedSigDigits = (int)Math.Round(data.BytesRegression.GetRegressionCurve().ValueAt(keySize) - .5);

            var baseNString = n.ToString().Substring(0, expectedSigDigits);
            var dynamicNDouble = double.Parse(BigInteger.Parse(n.ToString().Substring(expectedSigDigits)).ToString());

            //Remove this block after testing
            var nStr = n.ToString();
            var totStr = realTotient.ToString();
            var actualShared = 0;
            for (; actualShared < n.ToString().Length; actualShared++) if (nStr[actualShared] != totStr[actualShared]) break;
            var target = BigInteger.Parse(realTotient.ToString().Substring(expectedSigDigits));

            var interval = data.MinRangeRegression.GetPredictionInterval(dynamicNDouble, .99);
            var min = interval.LowerBound < 0 ? 0 : new BigInteger(interval.LowerBound);
            var max = new BigInteger(interval.UpperBound);

            Console.WriteLine(interval);
            Console.WriteLine("Number Of Logical Processors: {0}", Environment.ProcessorCount);

            if (debug)
            {
                Console.WriteLine("\n      N: " + nStr.Substring(actualShared));
                Console.WriteLine("Totient: " + totStr.Substring(actualShared));
                Console.WriteLine("Predicted Shared: " + expectedSigDigits);
                Console.WriteLine("Actual Shared: " + actualShared);
                Console.WriteLine("Diff: " + double.Parse(BigInteger.Subtract(n, realTotient).ToString()));
                //Console.WriteLine("Estimated Diff Magnitude: " + mag);
                //Console.WriteLine(max >= target && target >= min);
            }

            var gpu = Gpu.Default;
            var threadCount = gpu.Device.Cores;

            var test = max.ToByteArray();
            var test2 = new BigInteger(test);
            var test3 = BigInteger.Compare(max, test2);

            void Cyclic(int id)
            {
                
                /*
                for (var i = BigInteger.Add(min,id); i <= max; i += threadCount)
                {
                    if (BigInteger.Compare(i, target) != 0) continue;
                    Console.WriteLine("Thread: " + id);
                    Console.WriteLine("Totient: " + i);
                    break;
                }
                */
            }

            gpu.For(0,threadCount,Cyclic);

            /*
            Parallel.ForEach(BigIntSequenceReverse(min, max), (i, state) =>
            {
                Console.WriteLine(i);
                if (BigInteger.Compare(i,target) != 0) return;
                totient = i;
                state.Break();
            });
            */

            return BigInteger.Parse(baseNString + totient);
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


        public static byte[] FromBigEndian(this byte[] p)
        {
            var q = p.Reverse().ToArray();
            return (p[0] < 128 ? q : q.Concat(new byte[] { 0 })).ToArray();
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

        private static bool IsPrime(BigInteger num)
        {
            var numStr = num.ToString();
            var lastChar = numStr[numStr.Length - 1];
            if (new List<char>{'0','2','4','5','6','8'}.Contains(lastChar))
            {
                return false;
            }

            return false;
        }

        private static async void AddToDatabase(List<XY> points)
        {
            using (var context = new PrimeContext())
            {
                context.Primes.AddRange(points);
                await context.SaveChangesAsync();
            }
        }
    }
}