using System.Numerics;

namespace CodeBreaker.Models
{
    public class XY
    {
        public int X { get; }
        public int Y { get; }

        public BigInteger P { get; }
        public BigInteger Q { get; }
        public BigInteger N { get; }
        public BigInteger Totient { get; }
        public double nDouble { get; set; }
        public int nMagnitude { get; set; }
        public double totDouble { get; set; }
        public int totMagnitude { get; set; }

        public XY(BigInteger p, BigInteger q, BigInteger n, BigInteger totient, int x,int y)
        {
            X = x;
            Y = y;
            N = n;
            P = p;
            Q = q;
            Totient = totient;
        }
    }
}