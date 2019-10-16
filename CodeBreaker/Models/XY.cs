using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows.Documents;
using Extreme.Mathematics;

namespace CodeBreaker.Models
{
    [Table("Keys")]
    public class XY
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        private byte[] P { get; }
        private byte[] Q { get; }
        public byte[] N { get; set; }
        public byte[] Totient { get; set; }
        public byte[] NDynamic { get; set; }
        public byte[] TotDynamic { get; set; }
        public double NDouble { get; set; }
        public double TotDouble { get; set; }
        public DateTime Timestamp { get; set; }
        public XY(BigInteger p, BigInteger q, BigInteger n, BigInteger totient, int x, DateTime timestamp)
        {
            X = x;
            N = n.ToByteArray();
            P = p.ToByteArray();
            Q = q.ToByteArray();
            Totient = totient.ToByteArray();
            Timestamp = timestamp;

            var nStr = BitConverter.ToString(N.Take(N.Length - 1).Reverse().ToArray()).Split('-');
            var totStr = BitConverter.ToString(Totient.Take(N.Length - 1).Reverse().ToArray()).Split('-');

            for (var i = 0; i < nStr.Length; i++)
            {
                if (nStr[i] == totStr[i]) continue;
                Y = i;
                break;
            }

            NDynamic = N.Take(N.Length - Y - 1).ToArray();
            TotDynamic = Totient.Take(Totient.Length - Y - 1).ToArray();
            NDouble = double.Parse(new BigInteger(NDynamic.Concat(new byte[] { 0x00 }).ToArray()).ToString());
            TotDouble = double.Parse(new BigInteger(TotDynamic.Concat(new byte[] { 0x00 }).ToArray()).ToString());

            /*
            Console.WriteLine("N");
            Console.WriteLine(BitConverter.ToString(N.Take(N.Length-1).Reverse().ToArray()));
            Console.WriteLine(BitConverter.ToString(NDynamic.Take(NDynamic.Length).Reverse().ToArray()));
            Console.WriteLine(new BigInteger(NDynamic.Concat(new byte[] { 0x00 }).ToArray()).ToString());
            Console.WriteLine("Tot");
            Console.WriteLine(BitConverter.ToString(Totient.Take(Totient.Length-1).Reverse().ToArray()));
            Console.WriteLine(BitConverter.ToString(TotDynamic.Take(TotDynamic.Length).Reverse().ToArray()));
            Console.WriteLine(new BigInteger(TotDynamic.Concat(new byte[] { 0x00 }).ToArray()).ToString());
            */
        }

        public void PrintForms()
        {
            Console.WriteLine("N:");
            //Console.WriteLine(string.Join("-",NDynamic.Reverse()));
            Console.WriteLine(BitConverter.ToString(N.Take(N.Length-1).Reverse().ToArray()));
            Console.WriteLine(BitConverter.ToString(NDynamic.Reverse().ToArray()));
            Console.WriteLine(new BigInteger(NDynamic.Concat(new byte[] { 0x00 }).ToArray()));
            Console.WriteLine(NDouble);
            Console.WriteLine("Tot:");
            //Console.WriteLine(string.Join("-", TotDynamic.Reverse()));
            Console.WriteLine(BitConverter.ToString(Totient.Take(Totient.Length-1).Reverse().ToArray()));
            Console.WriteLine(BitConverter.ToString(TotDynamic.Reverse().ToArray()));
            Console.WriteLine(new BigInteger(TotDynamic.Concat(new byte[] { 0x00 }).ToArray()));
            Console.WriteLine(TotDouble);
            Console.WriteLine(Y);
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

        public double GetNDouble(int bytes)
        {
            return double.Parse(new BigInteger(N.Take(N.Length - bytes - 1).Concat(new byte[] { 0x00 }).ToArray()).ToString());
        }

        public double GetTotDouble(int bytes)
        {
            return double.Parse(new BigInteger(Totient.Take(Totient.Length - bytes - 1).Concat(new byte[] { 0x00 }).ToArray()).ToString());
        }
    }
}