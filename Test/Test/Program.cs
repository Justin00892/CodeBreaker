using Hybridizer.Runtime.CUDAImports;
using System;
using System.Threading;
using System.Threading.Tasks;
using Extreme.Mathematics;

namespace Test
{
    class Program
    {
        [EntryPoint]
        public static void Run()
        {
            var threadCount = Environment.ProcessorCount;
            var threads = new Thread[threadCount];
            /*
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
            */
        }

        static void Main(string[] args)
        {
            int[] a = { 1, 2, 3, 4, 5 };
            int[] b = { 10, 20, 30, 40, 50 };

            cudaDeviceProp prop;
            cuda.GetDeviceProperties(out prop, 0);
            //if .SetDistrib is not used, the default is .SetDistrib(prop.multiProcessorCount * 16, 128)
            HybRunner runner = HybRunner.Cuda();

            // create a wrapper object to call GPU methods instead of C#
            dynamic wrapped = runner.Wrap(new Program());

            wrapped.Run();

            Console.Out.WriteLine("DONE");
        }
    }
}
