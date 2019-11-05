using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using CodeBreaker.Models;
using Extreme.Mathematics;
using Extreme.Statistics;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Color = System.Drawing.Color;

namespace CodeBreaker
{
    public partial class SingleKeySizeForm : Form
    {
        private readonly int _size;
        private SimpleRegressionModel _regression;
        public SingleKeySizeForm(int size, IEnumerable<XY> points)
        {
            InitializeComponent();
            _size = size;

            var mapper = Mappers.Xy<XY>()
                .X(model => model.NDouble)
                .Y(model => model.TotDouble)
                .Fill(model => model.Y == ChartValues.GroupBy(p => p.Y)
                        .OrderByDescending(gp => gp.Count()).Select(p => p.Key).FirstOrDefault()
                    ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 150, 0))
                    : model.Y < ChartValues.GroupBy(p => p.Y)
                        .OrderByDescending(gp => gp.Count()).Select(p => p.Key).FirstOrDefault()
                    ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 0, 0))
                    : new SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 100, 0)));

            ChartValues = new ChartValues<XY>();
            RegressionValues = new ChartValues<ObservablePoint>();
            UpperValues = new ChartValues<ObservablePoint>();
            LowerValues = new ChartValues<ObservablePoint>();
            NToNValues = new ChartValues<ObservablePoint>();
            XAxis = new ChartValues<ObservablePoint>();
            YAxis = new ChartValues<ObservablePoint>();
            versusChart.Series = new SeriesCollection(mapper)
            {
                new ScatterSeries
                {
                    Values = ChartValues,
                    PointGeometry = DefaultGeometries.Diamond
                },
                new LineSeries
                {
                    Values = RegressionValues,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 0, 0))
                },
                new LineSeries
                {
                    Values = UpperValues,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 0, 0))
                },
                new LineSeries
                {
                    Values = LowerValues,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 0, 0))
                },
                /*
                new LineSeries
                {
                    Values = NToNValues,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 200))
                },
                */
                new LineSeries
                {
                    Values = XAxis,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0))
                },
                new LineSeries
                {
                    Values = YAxis,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 0))
                }
            };
            versusChart.DataClick += (sender, point) =>
            {
                var valuePoint = ChartValues.FirstOrDefault(v => v.NDouble == point.X && v.TotDouble == point.Y);
                if (valuePoint != null)
                {
                    Console.WriteLine(valuePoint.ToString());
                    Console.WriteLine("Range: " + _regression.GetPredictionInterval(valuePoint.NDouble,.99));
                }
            };
            versusChart.Zoom = ZoomingOptions.Xy;
            versusChart.DisableAnimations = true;
            versusChart.DataTooltip = null;
            ChartValues.AddRange(points);

            CalculateRegression();

            graphTabPanel.SelectedIndexChanged += (sender, args) =>
            {
                if (graphTabPanel.SelectedIndex == 1)
                    BuildPolar();
            };

            Timer = new Timer
            {
                Interval = 10000
            };
            Timer.Tick += (sender, args) => addButton.PerformClick();
        }

        private ChartValues<XY> ChartValues { get; }
        private ChartValues<ObservablePoint> RegressionValues { get; }
        private ChartValues<ObservablePoint> UpperValues { get; }
        private ChartValues<ObservablePoint> LowerValues { get; }
        private ChartValues<ObservablePoint> NToNValues { get; }
        private ChartValues<ObservablePoint> XAxis { get; }
        private ChartValues<ObservablePoint> YAxis { get; }
        private Timer Timer { get; }
        private bool IsRunning { get; set; }
        private async void AddButton_Click(object sender, EventArgs e)
        {
            addButton.Enabled = false;
            var data = await Task<List<XY>>.Factory.StartNew(() => Crypto.CompareNWithTotient(_size, _size, 100, false,true));
            var newMax = data.Max(p => p.NDouble) > ChartValues.Max(p => p.NDouble);
            ChartValues.AddRange(data);
            CalculateRegression();

            addButton.Enabled = true;
        }

        private void CalculateRegression()
        {
            var x = Vector.Create(ChartValues.Select(v => v.NDouble).ToArray());
            var y = Vector.Create(ChartValues.Select(v => v.TotDouble).ToArray());
            _regression = new SimpleRegressionModel(y, x);
            _regression.Fit();

            UpperValues.Clear();
            LowerValues.Clear();
            RegressionValues.Clear();
            NToNValues.Clear();
            XAxis.Clear();
            YAxis.Clear();

            var max = ChartValues.Max(p => p.NDouble);
            XAxis.Add(new ObservablePoint(0, 0));
            XAxis.Add(new ObservablePoint(0, max));
            YAxis.Add(new ObservablePoint(0, 0));
            YAxis.Add(new ObservablePoint(max,0));



            NToNValues.Add(new ObservablePoint(0,0));
            NToNValues.Add(new ObservablePoint(max,max));

            var ci = _regression.GetPredictionInterval(0, .99);
            UpperValues.Add(new ObservablePoint(0, ci.UpperBound));
            LowerValues.Add(new ObservablePoint(0, ci.LowerBound));
            RegressionValues.Add(new ObservablePoint(0, ci.Center));

            ci = _regression.GetPredictionInterval(max, .99);
            UpperValues.Add(new ObservablePoint(max, ci.UpperBound));
            LowerValues.Add(new ObservablePoint(max, ci.LowerBound));
            RegressionValues.Add(new ObservablePoint(max, ci.Center));

            var above = 0.0;
            var below = 0.0;
            foreach (var point in ChartValues)
            {
                var interval = _regression.GetPredictionInterval(point.NDouble, .99);
                if (point.TotDouble <= interval.UpperBound && point.TotDouble > interval.Center)
                    above++;
                else if(point.TotDouble <= interval.Center && point.TotDouble >= interval.LowerBound)
                    below++;
            }
            Console.WriteLine("Above Center: " + above/ChartValues.Count);
            Console.WriteLine("Below Center: " + below/ChartValues.Count);
        }

        private void BuildPolar()
        {
            var polarMapper = Mappers.Polar<XY>()
                .Radius(model => model.NDouble)
                .Angle(model => model.TotDouble)
                .Fill(model => model.Y == ChartValues.GroupBy(p => p.Y)
                                   .OrderByDescending(gp => gp.Count()).Select(p => p.Key).FirstOrDefault()
                    ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 150, 0))
                    : model.Y < ChartValues.GroupBy(p => p.Y)
                          .OrderByDescending(gp => gp.Count()).Select(p => p.Key).FirstOrDefault()
                        ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 0, 0))
                        : new SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 100, 0)));

            polarChart.Series = new SeriesCollection(polarMapper)
            {
                new ScatterSeries
                {
                    Values = ChartValues,
                    PointGeometry = DefaultGeometries.Circle
                }
            };
            polarChart.Zoom = ZoomingOptions.Xy;
            polarChart.DisableAnimations = true;
            polarChart.DataTooltip = null;
        }

        private void ContinuousButton_Click(object sender, EventArgs e)
        {
            IsRunning = !IsRunning;

            if (!IsRunning)
            {
                Timer.Start();
                continuousButton.Text = "Stop";
                continuousButton.BackColor = Color.Red;
            }
            else
            {
                Timer.Stop();
                continuousButton.Text = "Run Continuously";
                continuousButton.BackColor = Color.Lime;
            }
                
        }
    }
}
