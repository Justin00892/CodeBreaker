using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Extreme.Mathematics;

namespace GPUBinaryTest
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            var results = new SortedDictionary<int, int>();
            for (var x = 0; x < 1000; x++)
            {
                using (var csp = new RSACryptoServiceProvider(512))
                {
                    var parameters = csp.ExportParameters(true);
                    var p = FromBigEndian(parameters.P);
                    var q = FromBigEndian(parameters.Q);
                    var n = BigInteger.Multiply(p, q);
                    var tot = BigInteger.Multiply(p - 1, q - 1);

                    var nStr = BitConverter.ToString(n.ToByteArray()).Split('-');
                    var totStr = BitConverter.ToString(tot.ToByteArray()).Split('-');



                    var test = n.ToByteArray();
                    test[test.Length - 1] = 0x00;
                    var temp = new BigInteger(test.ToArray());

                    var i = nStr.Length-1;
                    for (; i >= 0; i--)
                        if (nStr[i] != totStr[i]) 
                            break;

                    if (!results.ContainsKey(i))
                        results[i] = 0;
                    results[i]++;
                    
                    Console.WriteLine("\n"+string.Join("-",nStr));
                    Console.WriteLine(string.Join("-",totStr));
                }
            }

            Console.WriteLine();
            foreach (var result in results)
                Console.WriteLine(result.Key +": "+result.Value);


            var target = BigInteger.Parse("256553263330763623874525398232939879603778600288284963502959397").ToByteArray();
            var array = new byte[target.Length];
            for (var i = 0; i < array.Length; i++)
                array[i] = new byte();

            while(array.GetHashCode() != target.GetHashCode())
            {
                array.IterateByte(0x02,array.Length-1);
                Console.WriteLine(string.Join("",BitConverter.ToString(array).Replace("-","").SkipWhile(c => c == '0')));
            }
        }

        private static void IterateByte(this byte[] array, byte step, int start)
        {
            if(array[start] == 255-(step-1))
                array.IterateByte(0x01,start - 1);
            array[start]+=step;
        }

        private static void IterateString(this IList<char> array,int step)
        {
            var carry = false;
            for (var i = array.Count - 1; carry || i == array.Count - 1; i--)
            {
                for (var x = 0; x < step; x++)
                {
                    carry = array[i] == 'F';
                    array[i] = carry ? array[i] = '0' : array[i] == '9' ? array[i] += (char) 8 : array[i] += (char) 1;
                }
            }
        }

        private static BigInteger FromBigEndian(byte[] p)
        {
            var q = p.Reverse().ToArray();
            return new BigInteger((p[0] < 128 ? q : q.Concat(new byte[] { 0 })).ToArray());
        }

        public static int SwapEndianness(int value)
        {
            var b1 = (value >> 0) & 0xff;
            var b2 = (value >> 8) & 0xff;
            var b3 = (value >> 16) & 0xff;
            var b4 = (value >> 24) & 0xff;

            return b1 << 24 | b2 << 16 | b3 << 8 | b4 << 0;
        } 
    }
}