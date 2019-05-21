using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

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

        public static List<Tuple<int,int>> CompareNWithTotient(int k)
        {
            var data = new List<Tuple<int, int>>();
            for (var i = 384; i < k; i+=8)
            {
                var csp = new RSACryptoServiceProvider(i);
                var parameters = csp.ExportParameters(true);
                var p = FromBigEndian(parameters.P);
                var q = FromBigEndian(parameters.Q);
                var n = BigInteger.Multiply(p, q);
                var tot = BigInteger.Multiply(p - 1, q - 1);
                //Console.WriteLine("N: "+ n);
                //Console.WriteLine("Totient: " + tot);

                var nStr = n.ToString();
                var totStr = tot.ToString();
                var j = 0;
                for (; j < nStr.Length; j++)
                {
                    if (nStr[j] != totStr[j]) break;
                }
                //Console.WriteLine("N Length: " + nStr.Length);
                //Console.WriteLine("Tot Length: " + totStr.Length);
                //Console.WriteLine("Significant digits in common: "+ j);
                data.Add(new Tuple<int, int>(nStr.Length,j));
            }

            return data;
        }

        public static BigInteger FromBigEndian(byte[] p)
        {
            var q = p.Reverse().ToArray();
            return new BigInteger((p[0] < 128 ? q : q.Concat(new byte[] { 0 })).ToArray());
        }
    }
}