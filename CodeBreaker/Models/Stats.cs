using System;
using System.Collections.Generic;
using System.Linq;
using Extreme.Mathematics;

namespace CodeBreaker.Models
{
    public class Stats
    {
        public List<XY> Points { get; }
        public BigFloat AverageRatio => BigFloat.Divide(Points.Sum(p => p.Ratio), Points.Count);
        public double DiffFactor => Points.Average(p => (double)p.X / p.GetDiffTuple().Item2);
        public Tuple<double, double> Regression => LinearRegression();
        public Stats()
        {
            Points = new List<XY>();
        }

        private Tuple<double,double> LinearRegression()
        {
            var n = Points.Count;
            var yMean = Points.Average(p => p.Y);
            var ySquares =
                from int val in Points.Select(p => p.Y)
                select (val - yMean) * (val - yMean);
            var ySum = ySquares.Sum();
            var sdY = Math.Sqrt(ySum / (n - 1));

            var xMean = Points.Average(p => p.X);
            var xSquares =
                from int val in Points.Select(p => p.X)
                select (val - xMean) * (val - xMean);
            var xSum = xSquares.Sum();
            var sdX = Math.Sqrt(xSum / (n - 1));

            var xySum = Points.Select(d => d.X * d.Y).Sum();
            var xSquareSum = Points.Sum(d => BigFloat.Multiply(d.X, d.X));
            var ySquareSum = Points.Sum(d => BigFloat.Multiply(d.Y, d.Y));
            var r = double.Parse(BigFloat.Divide(xySum,BigFloat.Sqrt(BigFloat.Multiply(xSquareSum, ySquareSum))).ToString());
            var slope = r * sdY / sdX;
            var intercept = yMean - slope * xMean;
            return new Tuple<double, double>(slope, intercept);
        }

        public void AddPoints(List<XY> points)
        {
            foreach (var point in points)
                if(Points.All(p => p.N != point.N)) Points.Add(point);
        }
    }
}