using System;
using System.Collections.Generic;
using System.Threading;
using ObjectModels.Models;
using BigInteger = Extreme.Mathematics.BigInteger;

namespace CryptoExternal
{
    class Crypto
    {
        [EntryPoint]
        public static BigInteger GuessTotient(Stats data, BigInteger n, int keySize, BigInteger e, BigInteger realTotient, bool debug)
        {
            var totient = BigInteger.MinusOne;
            var expectedSigDigits = (int)Math.Round(data.SigDigitsRegression.GetRegressionCurve().ValueAt(keySize) - .5);

            var baseNString = n.ToString().Substring(0, expectedSigDigits);
            var dynamicNDouble = Double.Parse((string) BigInteger.Parse(n.ToString().Substring(expectedSigDigits)).ToString());

            //Remove this block after testing
            var nStr = n.ToString();
            var totStr = realTotient.ToString();
            var actualShared = 0;
            for (; actualShared < n.ToString().Length; actualShared++) if (nStr[actualShared] != totStr[actualShared]) break;
            var target = BigInteger.Parse(realTotient.ToString().Substring(expectedSigDigits));

            var interval = data.MinRangeRegression.GetPredictionInterval(dynamicNDouble, .99);
            var min = interval.LowerBound < 0 ? 0 : new BigInteger(interval.LowerBound);
            var max = new BigInteger(interval.UpperBound);

            Console.WriteLine((object) interval);
            Console.WriteLine("Number Of Logical Processors: {0}", Environment.ProcessorCount);

            if (debug)
            {
                Console.WriteLine("\n      N: " + nStr.Substring(actualShared));
                Console.WriteLine("Totient: " + totStr.Substring(actualShared));
                Console.WriteLine("Predicted Shared: " + expectedSigDigits);
                Console.WriteLine("Actual Shared: " + actualShared);
                Console.WriteLine("Diff: " + Double.Parse((string) BigInteger.Subtract(n, realTotient).ToString()));
                //Console.WriteLine("Estimated Diff Magnitude: " + mag);
                //Console.WriteLine(max >= target && target >= min);
            }

            var threadCount = Environment.ProcessorCount;
            var threads = new List<Thread>();
            for (var x = 0; x < threadCount; x++)
            {
                var t = new Thread(id =>
                {
                    for (var i = min + (int)id; i <= max; i += threadCount)
                    {
                        if (BigInteger.Compare(i, target) != 0) continue;
                        Console.WriteLine("Thread: " + id);
                        Console.WriteLine("Totient: " + i);
                        break;
                    }
                });
                threads.Add(t);
            }

            for (var i = 0; i < threadCount; i++)
                threads[i].Start(i);
            foreach (var thread in threads)
                thread.Join();

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
    }
}
