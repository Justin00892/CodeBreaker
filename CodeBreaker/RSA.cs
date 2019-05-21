using System;
using System.Linq;
using System.Security.Cryptography;
using System.Numerics;

namespace CodeBreaker
{
    public static class RSA
    {
        private static double _minChance = .95;
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

        public static Tuple<string,BigInteger> RSAAttack(BigInteger ciphertext, BigInteger? publicKey, BigInteger? n)
        {         
            var output = "";
            var privateKey = BigInteger.Zero;
            if (publicKey == null || n == null) return new Tuple<string, BigInteger>(ciphertext+"",privateKey);
            var size = n.Value.ToByteArray().Length / 2;
            do
            {

            } while (Crypto.DecryptChance(output) < _minChance);

            return new Tuple<string,BigInteger>(output,privateKey);
        }

        public static Tuple<BigInteger,BigInteger, BigInteger> GenerateKeys(int size, bool debug)
        {
            var csp = new RSACryptoServiceProvider(size);
            var parameters = csp.ExportParameters(true);
            var p = Crypto.FromBigEndian(parameters.P);
            var q = Crypto.FromBigEndian(parameters.Q);
            
            var n = BigInteger.Multiply(p,q);
            var tot = BigInteger.Multiply(p - 1, q - 1);
            BigInteger e = 65537;
            /*
            do e = GetRandomBigInt(size);
            while (1 >= e || e >= tot || BigInteger.GreatestCommonDivisor(e,tot) != 1);
            */
            var d = e.ModInverse(tot);

            if (debug)
            {
                var test = new BigInteger(1234);
                var testOut = BigInteger.ModPow(test, e, n);
                Console.WriteLine(test);
                Console.WriteLine(testOut);
                Console.WriteLine(BigInteger.ModPow(testOut, d, n));
            }
            

            return new Tuple<BigInteger, BigInteger, BigInteger>(e,d,n);
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

        
    }
}