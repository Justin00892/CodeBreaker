using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CodeBreaker.Models;
using Extreme.Mathematics;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using SeriesCollection = LiveCharts.SeriesCollection;
using Stats = ObjectModels.Models.Stats;

namespace CodeBreaker
{
    public partial class KeyGenForm : Form
    {
        private int _maxSize = 512;
        private int _minSize = 384;
        private int _replicates = 100;
        private readonly Stats _data;

        public KeyGenForm()
        {
            InitializeComponent();
            _data = new Stats();
            distanceChart.Series.Clear();
            sizeChart.Series.Clear();
            versusChart.Series.Clear();

            //set up Size Chart
            sizeChart.DataTooltip = null;
            sizeChart.Hoverable = false;
            sizeChart.DisableAnimations = true;

            //set up versus chart
            versusChart.LegendLocation = LegendLocation.Right;
            versusChart.DisableAnimations = true;
            versusChart.Hoverable = false;
            versusChart.DataTooltip = null;
        }

        private async void RunButton_Click(object sender, EventArgs e)
        {
            runButton.Enabled = false;
            //loadingPanel.BringToFront();
            //loadingPanel.Visible = true;
            var enteredMaxSize = maxKeySizeBox.Value;
            var enteredMinSize = minKeySizeBox.Value;
            if (enteredMaxSize < 384 || enteredMaxSize > 16384 || enteredMinSize < 384 || enteredMinSize > 16384) keyWarningLabel.Text = "Range: 384 - 16384";
            else if (enteredMaxSize % 8 != 0)
                keyWarningLabel.Text = "Must be increments of 8";
            else if (enteredMinSize > enteredMaxSize)
                keyWarningLabel.Text = "Min Key Size must be less than Max Key Size";
            else
            {
                keyWarningLabel.Text = "";
                _maxSize = (int)enteredMaxSize;
                _minSize = (int) enteredMinSize;
                _replicates = (int) replicatesBox.Value;
                var points = await Task<List<XY>>.Factory.StartNew(() => Crypto.CompareNWithTotient(_minSize, _maxSize, _replicates, false));

                _data.AddPoints(points);
                MakeGraph(points);
            }
            runButton.Enabled = true;
        }

        private void MakeGraph(IReadOnlyCollection<XY> points)
        {
            var series = sizeChart.Series.FirstOrDefault(s => s.Title == "Data") ?? new ScatterSeries
            {
                Title = "Data",
                Values = new ChartValues<ObservablePoint>()
            };
            series.Values.AddRange(points.Select(p => new ObservablePoint(p.X,p.Y)));
            sizeChart.Series.Add(series);

            var mapper = Mappers.Xy<ObservablePoint>()
                    .X(p => Math.Log(p.X, 10))
                    .Y(p => Math.Log(p.Y, 10));
            var collection = new SeriesCollection(mapper);
            for (var i = _minSize; i <= _maxSize; i += 8)
            {
                var size = i;
                var nSeries = new ScatterSeries
                {
                    Title = "Size: " + size,
                    Values = new ChartValues<ObservablePoint>(),
                    PointGeometry = DefaultGeometries.Diamond
                };
                nSeries.Values.AddRange(points.Where(p => p.X == size)
                    .Select( p => new ObservablePoint(p.NDouble,p.TotDouble)));
                collection.Add(nSeries);
            }

            versusChart.DataClick += (sender, point) =>
            {
                var size = int.Parse(point.SeriesView.Title.Split(' ')[1]);
                var singleKeySizeForm = new SingleKeySizeForm(size,_data.Points.Where(p => p.X == size));
                singleKeySizeForm.ShowDialog();
            };

            versusChart.Series = collection;
        }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            testButton.Enabled = false;
            var keySize = 384;
            var (publicKey, _, n, realTotient) = await Task<Tuple<BigInteger, BigInteger, BigInteger,BigInteger>>.Factory
                .StartNew(() => RSA.GenerateKeys(keySize,false));
            //only for testing

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var totientGuess = await Task<BigInteger>.Factory.StartNew(() =>
            {
                return Crypto.GuessTotient(_data, n, keySize, publicKey, realTotient, false);
            });
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            testButton.Enabled = true;
        }
    }
}
