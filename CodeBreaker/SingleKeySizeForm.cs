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
                .Y(model => model.TotDouble);
            Charting.For<XY>(mapper);

            ChartValues = new Dictionary<int,ChartValues<XY>>();
            RegressionValues = new ChartValues<ObservablePoint>();
            UpperValues = new ChartValues<ObservablePoint>();
            LowerValues = new ChartValues<ObservablePoint>();
            NToNValues = new ChartValues<ObservablePoint>();
            XAxis = new ChartValues<ObservablePoint>();
            YAxis = new ChartValues<ObservablePoint>();
            versusChart.Series = new SeriesCollection
            {
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
                new LineSeries
                {
                    Values = NToNValues,
                    PointGeometry = null,
                    Fill = Brushes.Transparent,
                    Stroke = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 200))
                },
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
                var instance = (XY) point.Instance;
                Console.WriteLine(instance.ToString());
                Console.WriteLine(_regression.GetPredictionInterval(point.X));
            };
            
            versusChart.Zoom = ZoomingOptions.Xy;
            versusChart.DisableAnimations = true;
            versusChart.DataTooltip = null;

            AddPoints(points);

            CalculateRegression();

            Timer = new Timer
            {
                Interval = 10000
            };
            Timer.Tick += (sender, args) => addButton.PerformClick();
        }

        private Dictionary<int,ChartValues<XY>> ChartValues { get; }
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
            var data = await Task<List<XY>>.Factory.StartNew(() => Crypto.CompareNWithTotient(_size, _size, 100, false,false));
            AddPoints(data);
            CalculateRegression();

            addButton.Enabled = true;
        }

        private void AddPoints(IEnumerable<XY> points)
        {
            foreach (var group in points.GroupBy(p => p.Y).OrderByDescending(g => g.Count()))
            {
                if (!ChartValues.ContainsKey(group.Key))
                {
                    ChartValues[group.Key] = new ChartValues<XY>();
                    versusChart.Series.Add(new ScatterSeries
                    {
                        Values = ChartValues[group.Key],
                        PointGeometry = DefaultGeometries.Diamond
                    });
                }
                ChartValues[group.Key].AddRange(group.Select(g => g));
            }
        }

        private void CalculateRegression()
        {
            var values = ChartValues.OrderByDescending(d => d.Key).First().Value;
            var x = Vector.Create(values.Select(v => v.NDouble).ToArray());
            var y = Vector.Create(values.Select(v => v.TotDouble).ToArray());
            _regression = new SimpleRegressionModel(y, x);
            _regression.Fit();

            UpperValues.Clear();
            LowerValues.Clear();
            RegressionValues.Clear();
            NToNValues.Clear();
            XAxis.Clear();
            YAxis.Clear();

            var max = values.Max(p => p.NDouble);
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

            var results = new SortedDictionary<int, int>();
            foreach (var point in values)
            {
                var range = _regression.GetPredictionInterval(point.NDouble);
                var r = new List<Tuple<int,double>>
                {
                    new Tuple<int, double>(1,Math.Abs(range.UpperBound - point.TotDouble)), 
                    new Tuple<int, double>(2,Math.Abs(range.Center - point.TotDouble)),
                    new Tuple<int, double>(3,Math.Abs(range.LowerBound - point.TotDouble))
                }.OrderBy(n => n.Item2).First();

                if (!results.ContainsKey(r.Item1))
                    results[r.Item1] = 0;
                results[r.Item1]++;
            }

            foreach (var result in results)
                Console.WriteLine(result.Key +": "+result.Value);
            
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
