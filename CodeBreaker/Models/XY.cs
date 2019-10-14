﻿using System;
using System.Globalization;
using System.Linq;
using Extreme.Mathematics;

namespace CodeBreaker.Models
{
    public class XY
    {
        public int X { get; }
        public int Y { get; }

        private byte[] P { get; }
        private byte[] Q { get; }
        public byte[] N { get; }
        public byte[] Totient { get; }
        public byte[] NDynamic { get; }
        public byte[] TotDynamic { get; }
        public double NDouble { get; }
        public double TotDouble { get; }
        public XY(BigInteger p, BigInteger q, BigInteger n, BigInteger totient, int x)
        {
            X = x;
            N = n.ToByteArray();
            P = p.ToByteArray();
            Q = q.ToByteArray();
            Totient = totient.ToByteArray();


            /*
            Console.WriteLine("\nN:");
            Console.WriteLine(n);
            //Console.WriteLine(string.Join("-",N.Reverse()));
            Console.WriteLine(BitConverter.ToString(N.Take(N.Length-1).Reverse().ToArray()));
            Console.WriteLine(new BigInteger(N.Take(N.Length-1).Concat(new byte[]{0x00}).ToArray()));
            Console.WriteLine("Tot:");
            Console.WriteLine(totient);
            //Console.WriteLine(string.Join("-",Totient.Reverse()));
            Console.WriteLine(BitConverter.ToString(Totient.Take(Totient.Length-1).Reverse().ToArray()));
            Console.WriteLine(new BigInteger(Totient.Take(Totient.Length-1).Concat(new byte[]{0x00}).ToArray()));
            */

            for (var i = N.Length - 1; i >= 0; i--)
            {
                if (N[i] == Totient[i]) continue;
                Y = i;
                break;
            }

            NDynamic = N.Take(N.Length - Y - 1).ToArray();
            TotDynamic = Totient.Take(Totient.Length - Y - 1).ToArray();

            /*
            Console.WriteLine("N:");
            Console.WriteLine(string.Join("-",NDynamic.Reverse()));
            Console.WriteLine(BitConverter.ToString(NDynamic.Reverse().ToArray()));
            Console.WriteLine(new BigInteger(NDynamic.Concat(new byte[] { 0x00 }).ToArray()));
            Console.WriteLine("Tot:");
            Console.WriteLine(string.Join("-", TotDynamic.Reverse()));
            Console.WriteLine(BitConverter.ToString(TotDynamic.Reverse().ToArray()));
            Console.WriteLine(new BigInteger(TotDynamic.Concat(new byte[] { 0x00 }).ToArray()));
            */
            
            NDouble = double.Parse(new BigInteger(NDynamic.Concat(new byte[] { 0x00 }).ToArray()).ToString());
            TotDouble = double.Parse(new BigInteger(TotDynamic.Concat(new byte[] { 0x00 }).ToArray()).ToString());
        }


        public override string ToString()
        {
            return "\nKey Size: " + X +
                   "\nP: " + string.Join("-", P) +
                   "\nQ: " + string.Join("-", Q) +
                   "\nFull: " +
                   "\n      N: " + string.Join("-", N) +
                   "\nTotient: " + string.Join("-", Totient) +
                   "\nShared Digits: " + Y +
                   "\nDynamic Portion: " +
                   "\n  N: " + string.Join("-", NDynamic) +
                   "\n     " + NDouble +
                   "\nTot: " + string.Join("-", TotDynamic) +
                   "\n     " + TotDouble;
        }
    }
}