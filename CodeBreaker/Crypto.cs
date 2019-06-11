using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;

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
                    var q = FromBigEndian(parameters.Q);
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
                if (debug)
                {
                    Console.WriteLine("P:\n"+xy.P);
                    Console.WriteLine("\nSubP:\n"+subP);
                    Console.WriteLine("Q:\n"+xy.Q);
                    Console.WriteLine("\nSubQ:\n"+subQ);
                    Console.WriteLine("N:\n"+xy.N);
                    Console.WriteLine("SubN:\n" + BigInteger.Multiply(subP,subQ));
                    Console.WriteLine("\nTotient:\n"+xy.Totient);
                    Console.WriteLine("SubTotient:\n" + BigInteger.Multiply(subP-1,subQ-1));
                }
            }
            return data;
        }

        public static void AttemptFactor(BigInteger n)
        {
            
        }

        public static BigInteger GetN(byte[] p, byte[] q)
        {
            return BigInteger.Multiply(FromBigEndian(p),FromBigEndian(q));
        }

        public static BigInteger GuessTotient(Stats data, BigInteger n, BigInteger e)
        {
            var totient = BigInteger.One;
            var nSize = n.ToString().Length;
            var expectedSigDigits =(int)(data.Intercept + data.Slope * nSize);
            
            var baseNMinString = n.ToString().Substring(0,expectedSigDigits);
            var temp = BigInteger.Parse(baseNMinString);
            temp++;
            var baseNMaxString = temp.ToString();

            for (var i = baseNMinString.Length; i <= nSize; i++) baseNMinString += "0";
            for (var i = baseNMaxString.Length; i <= nSize; i++) baseNMaxString += "0";

            var min = BigInteger.Parse(baseNMinString);
            var max = BigInteger.Parse(baseNMaxString);

            Parallel.ForEach(BigIntSequence(min, max), i => Console.WriteLine(i));

            return totient;
        }

        public static IEnumerable<BigInteger> BigIntSequence(BigInteger min,BigInteger max)
        {
            var bi = min;
            while (bi<max)
            {
                yield return bi;
                bi += 1;     
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
    }
}