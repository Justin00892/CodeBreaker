using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
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

        public static byte[] GuessTotient(Stats data, byte[] n, int keySize, byte[] e, byte[] realTot, bool debug)
        {
            /*
            var range = data.BytesRegression.GetPredictionInterval(keySize);
            for (var i = Math.Floor(range.LowerBound); i <= Math.Ceiling(range.UpperBound); i++)
            {
                Console.WriteLine(i);
            }
            */
            var totient = n;
            var minSharedBytes = (int)Math.Round(data.BytesRegression().GetPredictionInterval(keySize).LowerBound);
            
            var nStr = BitConverter.ToString(n.Take(n.Length - 1).Reverse().ToArray()).Split('-');
            var totStr = BitConverter.ToString(realTot.Take(realTot.Length - 1).Reverse().ToArray()).Split('-');
            var actualShared = 0;
            for (var i = 0; i < nStr.Length; i++)
            {
                if (nStr[i] == totStr[i]) continue;
                actualShared = i;
                break;
            }

            Console.WriteLine(minSharedBytes);
            Console.WriteLine(data.BytesRegression().GetPredictionInterval(keySize));
            Console.WriteLine(actualShared);

            var dynamicN = n.Take(n.Length - minSharedBytes - 1).ToArray();
            Console.WriteLine("N");
            Console.WriteLine(BitConverter.ToString(n.Take(n.Length-1).Reverse().ToArray()));
            Console.WriteLine(BitConverter.ToString(dynamicN.Reverse().ToArray()));

            //Remove block after testing
            var target = realTot.Take(realTot.Length - minSharedBytes - 1).ToArray();
            Console.WriteLine("Tot");
            Console.WriteLine(BitConverter.ToString(realTot.Take(realTot.Length-1).Reverse().ToArray()));
            Console.WriteLine(BitConverter.ToString(target.Reverse().ToArray()));

            var nDouble = double.Parse(new BigInteger(dynamicN.Concat(new byte[] {0x00}).ToArray()).ToString());

            Console.WriteLine("N (double): "+nDouble);
            var interval = data.MinRangeRegression(keySize).GetPredictionInterval(nDouble, .99);
            var min = new BigInteger(interval.LowerBound);
            var minArray = min.ToByteArray();
            var max = new BigInteger(interval.UpperBound);
            var maxArray = max.ToByteArray();
            var mid = new BigInteger(interval.Center);
            var midArray = mid.ToByteArray();

            var num_threads = Environment.ProcessorCount;
            var result = new byte[midArray.Length];
            var lck = new object();
            var cts = new CancellationTokenSource();
            var tasks = new List<Task>(num_threads);

            var minDiff = BigInteger.Divide(min, num_threads/2);
            var maxDiff = BigInteger.Divide(max, num_threads/2);
            var range = new List<byte[]>();
            for (var i = 0; i < num_threads/2; i++)
            {
                range.Add(BigInteger.Add(mid,BigInteger.Multiply(maxDiff,i)).ToByteArray());
                range.Add(BigInteger.Subtract(mid,BigInteger.Multiply(minDiff,i)).ToByteArray());
            }

            for (var x = 0; x < num_threads; x++)
            {
                if (x % 2 == 0)
                {
                    tasks[x] = Task.Factory.StartNew(m =>
                    {
                        var array = new List<byte>((byte[])m).ToArray();
                        while (true)
                        {
                            /*
                            try
                            {
                                array.IterateByteUp(0x02, 0);
                                //Console.WriteLine(BitConverter.ToString(array.Reverse().ToArray()));
                                if (!array.SequenceEqual(target) && !array.SequenceEqual(maxArray)) continue;
                                lock (lck) result = array;
                                cts.Cancel();
                            }
                            catch (Exception)
                            {
                                break;
                            }
                            */
                        }
                    },range[x], cts.Token);
                    Console.WriteLine("Task "+ x + " started");
                }
                else
                {
                    tasks[x] = Task.Factory.StartNew(m =>
                    {
                        var array = new List<byte>((byte[])m).ToArray();
                        while (true)
                        {
                            /*
                            try
                            {
                                array.IterateByteDown(0x02, 0);
                                //Console.WriteLine(BitConverter.ToString(array.Reverse().ToArray()));
                                if (!array.SequenceEqual(target) && !array.SequenceEqual(minArray)) continue;
                                lock (lck) result = array;
                                cts.Cancel();
                            }
                            catch (Exception)
                            {
                                break;
                            }
                            */
                        }
                    },range[x], cts.Token);
                    Console.WriteLine("Task "+ x + " started");
                }
            }

            Task.WaitAll(tasks.ToArray());
            Console.WriteLine(BitConverter.ToString(result.Reverse().ToArray()));


            return totient;
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
            if (new BigInteger[] {0, 1, 2, 3, 5}.Contains(num)) return true;
            return (num - 1) % 6 == 0 || (num - 5) % 6 == 0;
        }

        private static void IterateByteUp(this byte[] array, byte step, int start)
        {
            if (array.All(b => b == 0xff)) throw new Exception("max reached");
            if(array[start] >= 256-step)
                array.IterateByteUp(0x01,start+1);
            array[start]+=step;
        }
        private static void IterateByteDown(this byte[] array, byte step, int start)
        {
            if (array.All(b => b == 0x00)) throw new Exception("min reached");
            if(array[start] <= 0+step)
                array.IterateByteDown(0x01,start+1);
            array[start]-=step;
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