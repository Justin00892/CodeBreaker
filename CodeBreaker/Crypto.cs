using System;
using System.Linq;
using System.Security.Cryptography;
using System.Numerics;
using System.Threading.Tasks;

namespace CodeBreaker
{
    public static class Crypto
    {
        private static RNGCryptoServiceProvider _rngProvider = new RNGCryptoServiceProvider();
        private static byte[] _bytes;
        private static Random _random = new Random(DateTime.Now.Millisecond);
        public static BigInteger RSAEncrypt(BigInteger input, BigInteger? publicKey, BigInteger? n)
        {
            if (publicKey == null || n == null) return input;
            Console.WriteLine(input.ToByteArray().Length);
            Console.WriteLine(publicKey.Value.ToByteArray().Length);

            var outputInt = BigInteger.ModPow(input, publicKey.Value, n.Value);
            return outputInt;
        }

        public static BigInteger RSADecrypt(BigInteger input, BigInteger? privateKey, BigInteger? n)
        {
            if (privateKey == null || n == null) return input;

            var outputInt = BigInteger.ModPow(input, privateKey.Value, n.Value);
            return outputInt;
        }

        public static Tuple<BigInteger,BigInteger, BigInteger> GenerateKeys(int size)
        {
            var csp = new RSACryptoServiceProvider(size);
            var parameters = csp.ExportParameters(true);
            var p = FromBigEndian(parameters.P);
            var q = FromBigEndian(parameters.Q);
            
            

            var n = BigInteger.Multiply(p,q);
;           var tot = BigInteger.Multiply(p - 1, q - 1);
            BigInteger e = 65537;
            /*
            do e = GetRandomBigInt(size);
            while (1 >= e || e >= tot || BigInteger.GreatestCommonDivisor(e,tot) != 1);
            */
            var d = e.ModInverse(tot);
            var test = new BigInteger(1234);
            var testOut = BigInteger.ModPow(test, e, n);
            Console.WriteLine(test);
            Console.WriteLine(testOut);
            Console.WriteLine(BigInteger.ModPow(testOut, d, n));

            return new Tuple<BigInteger, BigInteger, BigInteger>(e,d,n);
        }

        private static bool PrimeTest(BigInteger n, int k, int size)
        {
            if (n < 2 || n % 2 == 0) return n == 2;
            var s = n - 1;
            while (s % 2 == 0) s >>= 1;

            var isPrime = true;
            Parallel.For(0, k, i =>
            {
                var a = GetRandomBigInt(size);
                while (a >= n) a = GetRandomBigInt(size);
                var temp = s;
                var mod = (BigInteger)1;
                for (var j = 0; j < temp; ++j) mod = mod * a % n;
                while (temp != n - 1 && mod != 1 && mod != n - 1)
                {
                    mod = mod * mod % n;
                    temp *= 2;
                }

                if (mod != n - 1 && temp % 2 == 0) isPrime = false;
            });

            return isPrime;
        }

        private static BigInteger GetRandomBigInt(int bits)
        {
            _bytes = new byte[bits/8];
            _rngProvider.GetBytes(_bytes);

            var bg = new BigInteger(_bytes);
            if (bg.Sign == -1) bg *= -1;

            return bg;
        }

        private static BigInteger ModInverse(this BigInteger a, BigInteger n)
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

        private static BigInteger FromBigEndian(byte[] p)
        {
            var q = p.Reverse().ToArray();
            return new BigInteger((p[0] < 128 ? q : q.Concat(new byte[] { 0 })).ToArray());
        }
    }
}