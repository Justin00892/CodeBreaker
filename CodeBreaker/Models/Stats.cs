using System;
using System.Collections.Generic;
using System.Linq;
using CodeBreaker.Models;

namespace CodeBreaker
{
    public class Stats
    {
        public List<XY> Points { get; }
        public double SizeSlope { get; private set; }
        public double SizeIntercept { get; private set; }
        public double DistanceSlope { get; private set; }
        public double DistanceIntercept { get; private set; }

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

        public void LinearRegressionSize()
        {
            var n = Points.Count;
            var xySum = (double)Points.Select(d => d.X * d.Y).Sum();
            var xSum = (double)Points.Select(d => d.X).Sum();
            var ySum = (double)Points.Select(d => d.Y).Sum();
            var xSquareSum = (double) Points.Select(d => d.X * d.X).Sum();
            SizeSlope = (n*xySum - xSum*ySum)/(n*xSquareSum - xSum*xSum);

            SizeIntercept = ySum / n - SizeSlope * (xSum / n);
        }
    }
}