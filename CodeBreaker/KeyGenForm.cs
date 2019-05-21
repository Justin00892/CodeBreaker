using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CodeBreaker
{
    public partial class KeyGenForm : Form
    {
        private int _size = 384;
        private int _numRuns;
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
                var data = await Task<List<Tuple<int,int>>>.Factory.StartNew(() => Crypto.CompareNWithTotient(_size));
                _numRuns++;
                MakeGraph(data);
            }

            runButton.Enabled = true;
        }

        private void MakeGraph(List<Tuple<int,int>> data)
        {
            var mean = data.Average(d => d.Item2);
            var squares =
                from int val in data.Select(d => d.Item2)
                select (val - mean) * (val - mean);
            var sum = squares.Sum();
            var std = Math.Sqrt(sum / (data.Count - 1));

            var series = dataChart.Series.Add("Run "+_numRuns + "\n s = "+ Math.Round(std,3));
            series.ChartType = SeriesChartType.Line;

            foreach (var xy in data)
            {
                series.Points.AddXY(xy.Item1,xy.Item2);
            }

            var yMin = dataChart.ChartAreas[0].AxisY.Minimum;
            var yMax = dataChart.ChartAreas[0].AxisY.Maximum;
            var dataMin = data.OrderBy(d => d.Item2).First()?.Item2;
            var dataMax = data.OrderByDescending(d => d.Item2).First()?.Item2;
            if(dataMin != null && (dataMin.Value < yMin || yMin == 0)) yMin = dataMin.Value - dataMin.Value % 10;
            if(dataMax != null && (dataMax.Value < yMax || yMax == 100)) yMax = dataMax.Value + dataMax.Value % 10;
            dataChart.ChartAreas[0].AxisY.Minimum = yMin;
            dataChart.ChartAreas[0].AxisY.Maximum = yMax;

            dataChart.Refresh();
        }
    }
}
