using System;
using System.Security.Cryptography;
using Extreme.Mathematics;

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

            var outputInt = BigInteger.ModularPow(input, publicKey.Value, n.Value);
            return outputInt;
        }

        public static BigInteger RSADecrypt(BigInteger input, BigInteger? privateKey, BigInteger? n)
        {
            if (privateKey == null || n == null) return input;

            var outputInt = BigInteger.ModularPow(input, privateKey.Value, n.Value);
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

        public static Tuple<BigInteger,BigInteger, BigInteger, BigInteger> GenerateKeys(int size, bool debug)
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
                var testOut = BigInteger.ModularPow(test, e, n);
                Console.WriteLine(test);
                Console.WriteLine(testOut);
                Console.WriteLine(BigInteger.ModularPow(testOut, d, n));
            }
            

            return new Tuple<BigInteger, BigInteger, BigInteger, BigInteger>(e,d,n,tot);
        }        
    }
}