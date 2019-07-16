using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBreaker
{
    public class Stats
    {
        public List<XY> Points { get; }
        public double Slope { get; private set; }
        public double Intercept { get; private set; }

        public Stats()
        {
            Points = new List<XY>();
        }
        public double StandardDeviation()
        {
            var mean = Points.Average(p => p.Y);
            var squares =
                from int val in Points.Select(p => p.Y)
                select (val - mean) * (val - mean);
            var sum = squares.Sum();
            return Math.Sqrt(sum / (Points.Count - 1));
        }

        public void LinearRegression()
        {
            var n = Points.Count;
            var xySum = (double)Points.Select(d => d.X * d.Y).Sum();
            var xSum = (double)Points.Select(d => d.X).Sum();
            var ySum = (double)Points.Select(d => d.Y).Sum();
            var xSquareSum = (double) Points.Select(d => d.X * d.X).Sum();
            Slope = (n*xySum - xSum*ySum)/(n*xSquareSum - xSum*xSum);

            Intercept = ySum / n - Slope * (xSum / n);
        }

    }
}