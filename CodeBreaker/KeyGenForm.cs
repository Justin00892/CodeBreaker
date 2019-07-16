using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Numerics;

namespace CodeBreaker
{
    public partial class KeyGenForm : Form
    {
        private int _size = 384;
        private Stats _data;
        public KeyGenForm()
        {
            InitializeComponent();
            dataChart.Series.Clear();
            distanceChart.Series.Clear();
        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            runButton.Enabled = false;
            var enteredSize = keySizeBox.Value;
            if (enteredSize < 384 || enteredSize > 16384) keyWarningLabel.Text = "Range: 384 - 16384";
            else if (enteredSize % 8 != 0) keyWarningLabel.Text = "Must be increments of 8";
            else
            {
                keyWarningLabel.Text = "";
                _size = (int)enteredSize;
                _data = await Task<Stats>.Factory.StartNew(() => Crypto.CompareNWithTotient(_size,10,true));
                MakeGraph(_data);
            }

            runButton.Enabled = true;
        }

        private void MakeGraph(Stats data)
        {
            var series = dataChart.Series.FindByName("Data") ?? dataChart.Series.Add("Data");
            series.ChartType = SeriesChartType.Point;

            foreach (var xy in data.Points)
                series.Points.AddXY(xy.X,xy.Y);

            rangeLabel.Text = "s = " + Math.Round(data.StandardDeviation(), 3);

            var yMin = dataChart.ChartAreas[0].AxisY.Minimum;
            var yMax = dataChart.ChartAreas[0].AxisY.Maximum;
            var yDataMin = data.Points.OrderBy(d => d.Y).First()?.Y;
            var yDataMax = data.Points.OrderByDescending(d => d.Y).First()?.Y;
            if(yDataMin != null && (yDataMin.Value < yMin || yMin == 0)) yMin = yDataMin.Value - yDataMin.Value % 10;
            if(yDataMax != null && (yDataMax.Value > yMax || yMax == 100)) yMax = yDataMax.Value + yDataMax.Value % 10;
            dataChart.ChartAreas[0].AxisY.Minimum = yMin;
            dataChart.ChartAreas[0].AxisY.Maximum = yMax;

            var regressionLine = dataChart.Series.FindByName("Regression Line") ?? dataChart.Series.Add("Regression Line");
            regressionLine.ChartType = SeriesChartType.Line;
            regressionLine.Points.Clear();

            var xMin = series.Points.FindMinByValue("X")?.XValue ?? 0;
            var xMax = series.Points.FindMaxByValue("X")?.XValue ?? 1;
            var minPoint = data.SizeIntercept + data.SizeSlope * xMin;
            var maxPoint = data.SizeIntercept + data.SizeSlope * xMax;
            regressionLine.Points.AddXY(xMin, minPoint);
            regressionLine.Points.AddXY(xMax, maxPoint);

            dataChart.Refresh();

            var distanceSeries = distanceChart.Series.FindByName("Data") ?? distanceChart.Series.Add("Data");
            distanceSeries.ChartType = SeriesChartType.Point;

            foreach (var xy in data.Points)
                distanceSeries.Points.AddXY(xy.X, xy.Diff);

            distanceChart.Refresh();
        }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            testButton.Enabled = false;
            var keys = await Task<Tuple<BigInteger, BigInteger, BigInteger,BigInteger>>.Factory
                .StartNew(() => RSA.GenerateKeys(384,false));
            var publicKey = keys.Item1;
            var n = keys.Item3;
            //only for testing
            var realTotient = keys.Item4;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var totientGuess = await Task<BigInteger>.Factory.StartNew(()=> Crypto.GuessTotient(_data,n,publicKey,realTotient));
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            testButton.Enabled = true;
        }
    }
}
