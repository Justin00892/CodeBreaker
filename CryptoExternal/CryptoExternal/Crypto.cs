using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Extreme.Mathematics;
using Hybridizer.Runtime.CUDAImports;
using Objects.Models;

namespace CryptoExternal
{
    public class CryptoExternal
    {
        [EntryPoint]
        public static BigInteger GuessTotient()
        {
            var totient = BigInteger.MinusOne;
            var threadCount = Environment.ProcessorCount;
            Console.WriteLine(threadCount);
            
            /*
            var threads = new Thread[threadCount];
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
                threads[x] = t;
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

            return totient;
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

        public static void Run(Stats data, BigInteger n, int keySize, BigInteger e, BigInteger realTotient, bool debug)
        {
            var expectedSigDigits = (int)Math.Round(data.SigDigitsRegression.GetRegressionCurve().ValueAt(keySize) - .5);

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
            
            if (debug)
            {
                Console.WriteLine("\n      N: " + nStr.Substring(actualShared));
                Console.WriteLine("Totient: " + totStr.Substring(actualShared));
                Console.WriteLine("Predicted Shared: " + expectedSigDigits);
                Console.WriteLine("Actual Shared: " + actualShared);
                Console.WriteLine("Diff: " + double.Parse(BigInteger.Subtract(n, realTotient).ToString()));
                //Console.WriteLine("Estimated Diff Magnitude: " + mag);
                //Console.WriteLine(max >= target && target >= min);
                Console.WriteLine("Range: " + interval);
            }

            cudaDeviceProp prop;
            cuda.GetDeviceProperties(out prop, 0);
            //if .SetDistrib is not used, the default is .SetDistrib(prop.multiProcessorCount * 16, 128)
            HybRunner runner = HybRunner.Cuda();

            // create a wrapper object to call GPU methods instead of C#
            dynamic wrapped = runner.Wrap(new CryptoExternal());

            wrapped.GuessTotient();
            //wrapped.GuessTotient(min,max,target);

            Console.Out.WriteLine("DONE");
        }
    }
}
