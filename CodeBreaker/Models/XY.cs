using System.Numerics;

namespace CodeBreaker
{
    public class XY
    {
        public int X { get; }
        public int Y { get; }

        public BigInteger P { get; }
        public BigInteger Q { get; }
        public BigInteger N { get; }
        public BigInteger Totient { get; }

        public XY(BigInteger p, BigInteger q, BigInteger n, BigInteger totient, int y)
        {
            X = n.ToString().Length;
            Y = y;
            N = n;
            P = p;
            Q = q;
            Totient = totient;
        }
    }
}