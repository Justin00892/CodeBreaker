using System.Collections.Generic;
using System.Linq;
using Extreme.Mathematics;
using Extreme.Statistics;

namespace CodeBreaker.Models
{
    public class Stats
    {
        public List<XY> Points { get; }
        public SimpleRegressionModel SigDigitsRegression => SigDigitsLinearRegression();
        public SimpleRegressionModel MinRangeRegression => MinRangeLinearRegression();
        public Stats()
        {
            Points = new List<XY>();
        }

        private SimpleRegressionModel SigDigitsLinearRegression()
        {
            var x = Vector.Create(Points.Select(v => (double)v.X).ToArray());
            var y = Vector.Create(Points.Select(v => (double)v.Y).ToArray());
            var regression = new SimpleRegressionModel(y, x);
            regression.Fit();

            return regression;
        }

        private SimpleRegressionModel MinRangeLinearRegression()
        {
            var x = Vector.Create(Points.Select(v => v.NDouble).ToArray());
            var y = Vector.Create(Points.Select(v => v.TotDouble).ToArray());
            var regression = new SimpleRegressionModel(y, x);
            regression.Fit();

            return regression;
        }

        public void AddPoints(IEnumerable<XY> points)
        {
            foreach (var point in points.Where(point => Points.All(p => p.N != point.N)))
                Points.Add(point);
        }
    }
}