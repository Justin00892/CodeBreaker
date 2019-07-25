using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using CodeBreaker.Models;
using Extreme.Mathematics;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Series = LiveCharts.Wpf.Series;
using SeriesCollection = LiveCharts.SeriesCollection;

namespace CodeBreaker
{
    public partial class KeyGenForm : Form
    {
        private int _size = 512;
        private int _replicates = 100;
        private readonly Stats _data;
        private List<ScatterSeries> _series = new List<ScatterSeries>();
        public KeyGenForm()
        {
            InitializeComponent();
            _data = new Stats();
            dataChart.Series.Clear();
            distanceChart.Series.Clear();
            nChart.Series.Clear();
            sizeChartLive.Series.Clear();
        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            runButton.Enabled = false;
            //loadingPanel.BringToFront();
            //loadingPanel.Visible = true;
            var enteredSize = keySizeBox.Value;
            if (enteredSize < 384 || enteredSize > 16384) keyWarningLabel.Text = "Range: 384 - 16384";
            else if (enteredSize % 8 != 0) keyWarningLabel.Text = "Must be increments of 8";
            else
            {
                keyWarningLabel.Text = "";
                _size = (int)enteredSize;
                _replicates = (int) replicatesBox.Value;
                var points = await Task<List<XY>>.Factory.StartNew(() => Crypto.CompareNWithTotient(384, _size, _replicates, false));
                _data.AddPoints(points);
                MakeGraph(_data);

            }

            //loadingPanel.Visible = false;
            //loadingPanel.SendToBack();
            runButton.Enabled = true;
        }

        private void MakeGraph(Stats data)
        {
            var series = sizeChartLive.Series.FirstOrDefault(s => s.Title == "Data") ?? new ScatterSeries
            {
                Title = "Data",
                Values = new ChartValues<ObservablePoint>()
            };
            foreach (var xy in data.Points)
                series.Values.Add(new ObservablePoint(xy.X, xy.Y));

            sizeChartLive.Series.Add(series);

            var mapper = Mappers.Xy<ObservablePoint>()
                .X(p => Math.Log(p.X, 10))
                .Y(p => Math.Log(p.Y, 10));

            var collection = new SeriesCollection(mapper);
            for (var i = 384; i <= _size; i += 8)
            {
                var nSeries = new ScatterSeries
                {
                    Title = "Size: " + i,
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Diamond
                };
                foreach (var xy in data.Points.Where(p => p.X == i))
                {
                    nSeries.Values.Add(new ObservablePoint(double.Parse(xy.NDynamic.ToString()),
                        double.Parse(xy.TotDynamic.ToString())));
                }
                _series.Add(nSeries);
                collection.Add(nSeries);
            }

            versusChart.DataClick += (sender, point) =>
            {
                var pSeries = _series.First(s => s.Values.GetPoints(s).Contains(point));
                pSeries.Visibility = Visibility.Collapsed;
            };

            versusChart.LegendLocation = LegendLocation.Right;
            //versusChart.AxisX[0].Title = "N";
            //versusChart.AxisY[0].Title = "Totient";
            versusChart.Zoom = ZoomingOptions.Xy;
            versusChart.Series = collection;
        }

        private void MakeGraphOld(Stats data)
        {
            var series = dataChart.Series.FindByName("Data") ?? dataChart.Series.Add("Data");
            series.ChartType = SeriesChartType.Point;

            foreach (var xy in data.Points)
                series.Points.AddXY(xy.X,xy.Y);

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
            var regression = data.Regression;
            var minPoint = regression.Item2 + regression.Item1 * xMin;
            var maxPoint = regression.Item2 + regression.Item1 * xMax;
            regressionLine.Points.AddXY(xMin, minPoint);
            regressionLine.Points.AddXY(xMax, maxPoint);

            dataChart.Refresh();

            var diffSeries = distanceChart.Series.FindByName("Data") ?? distanceChart.Series.Add("Data");
            diffSeries.ChartType = SeriesChartType.Point;

            foreach (var xy in data.Points)
                diffSeries.Points.AddXY(xy.X, xy.Diff);

            distanceChart.Refresh();

            nChart.GetToolTipText += (sender, args) =>
            {
                switch (args.HitTestResult.ChartElementType)
                {
                    case ChartElementType.DataPoint:
                        var point = args.HitTestResult.Series.Points[args.HitTestResult.PointIndex];
                        args.Text = $"N:\t{point.XValue}\nTot:\t{point.YValues[0]}";
                        break;
                }
            };
            for (var i = 384; i <= _size; i += 8)
            {
                var nSeries = nChart.Series.FindByName("Size: " + i) ?? nChart.Series.Add("Size: " + i);
                nSeries.ChartType = SeriesChartType.Point;

                foreach (var xy in data.Points.Where(p => p.X == i))
                    nSeries.Points.AddXY(double.Parse(xy.NDynamic.ToString()), double.Parse(xy.TotDynamic.ToString()));
            }
            
            nChart.Refresh();
        }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            testButton.Enabled = false;
            var keySize = 384;
            var keys = await Task<Tuple<BigInteger, BigInteger, BigInteger,BigInteger>>.Factory
                .StartNew(() => RSA.GenerateKeys(keySize,false));
            var publicKey = keys.Item1;
            var n = keys.Item3;
            //only for testing
            var realTotient = keys.Item4;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var totientGuess = await Task<BigInteger>.Factory.StartNew(()=> Crypto.GuessTotient(_data,n,keySize,publicKey,realTotient));
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            testButton.Enabled = true;
        }
    }
}
