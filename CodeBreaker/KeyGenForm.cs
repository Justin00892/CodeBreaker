using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CodeBreaker
{
    public partial class KeyGenForm : Form
    {
        private int _size = 384;
        public KeyGenForm()
        {
            InitializeComponent();
            dataChart.Series.Clear();
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
                var data = await Task<Stats>.Factory.StartNew(() => Crypto.CompareNWithTotient(_size,10,false));
                MakeGraph(data);

                /*
                var keys = await Task<Tuple<BigInteger, BigInteger, BigInteger>>.Factory
                    .StartNew(() => RSA.GenerateKeys(_size,false));
                var publicKey = keys.Item1;
                var n = keys.Item3;

                var totientGuess = await Task<BigInteger>.Factory.StartNew(()=> Crypto.GuessTotient(data,n,publicKey));
                */
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
            var minPoint = data.Intercept + data.Slope * xMin;
            var maxPoint = data.Intercept + data.Slope * xMax;
            regressionLine.Points.AddXY(xMin, minPoint);
            regressionLine.Points.AddXY(xMax, maxPoint);

            dataChart.Refresh();
        }
    }
}
