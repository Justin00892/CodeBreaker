using System;
using System.Globalization;
using Extreme.Mathematics;

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
        public BigFloat Ratio { get; }
        public double Diff { get; }
        public XY(BigInteger p, BigInteger q, BigInteger n, BigInteger totient, int x,int y)
        {
            X = x;
            Y = y;
            N = n;
            P = p;
            Q = q;
            Totient = totient;
            Ratio = BigFloat.Divide(Totient, N);
            Diff = double.Parse(BigInteger.Subtract(N, Totient).ToString());
        }

        public Tuple<double, int> GetDiffTuple()
        {
            var diffSplit = Diff.ToString(CultureInfo.InvariantCulture).Split('E');
            return new Tuple<double, int>(double.Parse(diffSplit[0]), int.Parse(diffSplit[1]));
        }
    }
}