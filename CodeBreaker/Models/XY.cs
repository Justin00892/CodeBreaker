using System;
using System.Globalization;
using Extreme.Mathematics;

namespace CodeBreaker.Models
{
    public class XY
    {
        public int X { get; }
        public int Y { get; }

        private BigInteger P { get; }
        private BigInteger Q { get; }
        public BigInteger N { get; }
        public BigInteger Totient { get; }
        private BigInteger NDynamic { get; }
        private BigInteger TotDynamic { get; }
        public BigFloat Ratio { get; }
        public double Diff { get; }
        public double NDouble { get; }
        public double TotDouble { get; }
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
            NDynamic = BigInteger.Parse(N.ToString().Substring(Y));
            TotDynamic = BigInteger.Parse(Totient.ToString().Substring(Y));
            NDouble = double.Parse(NDynamic.ToString());
            TotDouble = double.Parse(TotDynamic.ToString());
        }

        public Tuple<double, int> GetDiffTuple()
        {
            var diffSplit = Diff.ToString(CultureInfo.InvariantCulture).Split('E');
            return new Tuple<double, int>(double.Parse(diffSplit[0]), int.Parse(diffSplit[1]));
        }

        public override string ToString()
        {
            return "\nKey Size: " + X +
                   "\nP: " + P +
                   "\nQ: " + Q +
                   "\nFull: " +
                   "\n      N: " + N +
                   "\nTotient: " + Totient +
                   "\nShared Digits: " + Y +
                   "\nDynamic Portion: " +
                   "\n  N: " + NDynamic +
                   "\nTot: " + TotDynamic +
                   "\nDifference: " + Diff;
        }
    }
}