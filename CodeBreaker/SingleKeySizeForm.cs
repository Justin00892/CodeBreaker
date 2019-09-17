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
                    : model.Y > ChartValues.GroupBy(p => p.Y)
                        .OrderByDescending(gp => gp.Count()).Select(p => p.Key).FirstOrDefault()
                    ? new SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 200, 0))
                    : null);
            Charting.For<XY>(mapper);

            ChartValues = new ChartValues<XY>();
            RegressionValues = new ChartValues<ObservablePoint>();
            versusChart.Series = new SeriesCollection
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
                    Fill = Brushes.Transparent
                }
            };
            versusChart.DataClick += (sender, point) =>
            {
                var valuePoint = ChartValues.FirstOrDefault(v => v.NDouble == point.X && v.TotDouble == point.Y);
                if(valuePoint != null) Console.WriteLine(valuePoint.ToString());
            };
            versusChart.Zoom = ZoomingOptions.Xy;
            versusChart.DisableAnimations = true;
            versusChart.DataTooltip = null;
            ChartValues.AddRange(points);
            CalculateRegression();

            graphTabPanel.SelectedIndexChanged += (sender, args) =>
            {
                if (graphTabPanel.SelectedIndex == 1)
                    BuildHistogram();
            };

            Timer = new Timer
            {
                Interval = 10000
            };
            Timer.Tick += (sender, args) => addButton.PerformClick();
        }

        private ChartValues<XY> ChartValues { get; }
        private ChartValues<ObservablePoint> RegressionValues { get; }
        private Timer Timer { get; }
        private bool IsRunning { get; set; }
        private async void AddButton_Click(object sender, EventArgs e)
        {
            addButton.Enabled = false;
            var data = await Task<List<XY>>.Factory.StartNew(() => Crypto.CompareNWithTotient(_size, _size, 100, false));
            var newMax = data.Max(p => p.NDouble) > ChartValues.Max(p => p.NDouble);
            ChartValues.AddRange(data);
            if(newMax) CalculateRegression();

            addButton.Enabled = true;
        }

        private void CalculateRegression()
        {
            var x = Vector.Create(ChartValues.Select(v => v.NDouble).ToArray());
            var y = Vector.Create(ChartValues.Select(v => v.TotDouble).ToArray());
            var regression = new SimpleRegressionModel(y,x);
            regression.Fit();
            var line = regression.GetRegressionLine();
            RegressionValues.Clear();
            RegressionValues.Add(new ObservablePoint(0,line.ValueAt(0)));
            var max = ChartValues.Max(p => p.NDouble);
            RegressionValues.Add(new ObservablePoint(max,line.ValueAt(max)));
        }

        private void BuildHistogram()
        {
            var max = Math.Ceiling(ChartValues.Max(p => p.TotDouble));
            var min = Math.Floor(ChartValues.Min(p => p.TotDouble));
            var binSize = max / 100;

            var histValues = new Dictionary<string,int>();
            for (var i = min; i < max; i += binSize)
                histValues.Add(i + " - " + (i + binSize), ChartValues.Count(v => v.TotDouble > i && v.TotDouble <= i + binSize));

            sizeChart.Series.Clear();
            sizeChart.AxisY.Clear();
            sizeChart.Series = new SeriesCollection
            {
                new RowSeries
                {
                    Values = new ChartValues<int>(histValues.Values)
                }
            };
            sizeChart.AxisY.Add(new Axis
            {
                Labels = histValues.Keys.ToList()
            });
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
