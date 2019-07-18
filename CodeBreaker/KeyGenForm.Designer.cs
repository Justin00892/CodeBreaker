namespace CodeBreaker
{
    partial class KeyGenForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.keySizeLabel = new System.Windows.Forms.Label();
            this.keyWarningLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.keySizeBox = new System.Windows.Forms.NumericUpDown();
            this.testButton = new System.Windows.Forms.Button();
            this.tabPanel = new System.Windows.Forms.TabControl();
            this.sizeDiffTab = new System.Windows.Forms.TabPage();
            this.dataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.distanceTab = new System.Windows.Forms.TabPage();
            this.distanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.keySizeBox)).BeginInit();
            this.tabPanel.SuspendLayout();
            this.sizeDiffTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).BeginInit();
            this.distanceTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.distanceChart)).BeginInit();
            this.SuspendLayout();
            // 
            // keySizeLabel
            // 
            this.keySizeLabel.AutoSize = true;
            this.keySizeLabel.Location = new System.Drawing.Point(10, 11);
            this.keySizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.keySizeLabel.Name = "keySizeLabel";
            this.keySizeLabel.Size = new System.Drawing.Size(71, 13);
            this.keySizeLabel.TabIndex = 0;
            this.keySizeLabel.Text = "Max Key Size";
            // 
            // keyWarningLabel
            // 
            this.keyWarningLabel.AutoSize = true;
            this.keyWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.keyWarningLabel.Location = new System.Drawing.Point(127, 11);
            this.keyWarningLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.keyWarningLabel.Name = "keyWarningLabel";
            this.keyWarningLabel.Size = new System.Drawing.Size(0, 13);
            this.keyWarningLabel.TabIndex = 2;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(10, 28);
            this.runButton.Margin = new System.Windows.Forms.Padding(2);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(112, 19);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Run Analysis";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // keySizeBox
            // 
            this.keySizeBox.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.keySizeBox.Location = new System.Drawing.Point(83, 9);
            this.keySizeBox.Margin = new System.Windows.Forms.Padding(2);
            this.keySizeBox.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.keySizeBox.Minimum = new decimal(new int[] {
            384,
            0,
            0,
            0});
            this.keySizeBox.Name = "keySizeBox";
            this.keySizeBox.Size = new System.Drawing.Size(39, 20);
            this.keySizeBox.TabIndex = 5;
            this.keySizeBox.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // testButton
            // 
            this.testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testButton.Location = new System.Drawing.Point(459, 9);
            this.testButton.Margin = new System.Windows.Forms.Padding(2);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(56, 19);
            this.testButton.TabIndex = 8;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // tabPanel
            // 
            this.tabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPanel.Controls.Add(this.sizeDiffTab);
            this.tabPanel.Controls.Add(this.distanceTab);
            this.tabPanel.Location = new System.Drawing.Point(12, 52);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(500, 305);
            this.tabPanel.TabIndex = 9;
            // 
            // sizeDiffTab
            // 
            this.sizeDiffTab.Controls.Add(this.dataChart);
            this.sizeDiffTab.Location = new System.Drawing.Point(4, 22);
            this.sizeDiffTab.Name = "sizeDiffTab";
            this.sizeDiffTab.Padding = new System.Windows.Forms.Padding(3);
            this.sizeDiffTab.Size = new System.Drawing.Size(492, 279);
            this.sizeDiffTab.TabIndex = 0;
            this.sizeDiffTab.Text = "Size";
            this.sizeDiffTab.UseVisualStyleBackColor = true;
            // 
            // dataChart
            // 
            chartArea1.AxisX.Title = "Modulus & Totient Size (bits)";
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.Title = "Common Significant Digits";
            chartArea1.AxisY2.IsMarginVisible = false;
            chartArea1.Name = "ChartArea1";
            this.dataChart.ChartAreas.Add(chartArea1);
            this.dataChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.dataChart.Legends.Add(legend1);
            this.dataChart.Location = new System.Drawing.Point(3, 3);
            this.dataChart.Margin = new System.Windows.Forms.Padding(2);
            this.dataChart.Name = "dataChart";
            this.dataChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.dataChart.Series.Add(series1);
            this.dataChart.Size = new System.Drawing.Size(486, 273);
            this.dataChart.TabIndex = 7;
            this.dataChart.Text = "dataChart";
            // 
            // distanceTab
            // 
            this.distanceTab.Controls.Add(this.distanceChart);
            this.distanceTab.Location = new System.Drawing.Point(4, 22);
            this.distanceTab.Name = "distanceTab";
            this.distanceTab.Padding = new System.Windows.Forms.Padding(3);
            this.distanceTab.Size = new System.Drawing.Size(492, 279);
            this.distanceTab.TabIndex = 1;
            this.distanceTab.Text = "Distance";
            this.distanceTab.UseVisualStyleBackColor = true;
            // 
            // distanceChart
            // 
            chartArea2.AxisX.Title = "Size (bits)";
            chartArea2.AxisY.MajorTickMark.Interval = 0D;
            chartArea2.AxisY.MajorTickMark.IntervalOffset = 0D;
            chartArea2.AxisY.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea2.AxisY.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea2.AxisY.Title = "Totient / N";
            chartArea2.AxisY2.IsMarginVisible = false;
            chartArea2.Name = "ChartArea1";
            this.distanceChart.ChartAreas.Add(chartArea2);
            this.distanceChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.distanceChart.Legends.Add(legend2);
            this.distanceChart.Location = new System.Drawing.Point(3, 3);
            this.distanceChart.Margin = new System.Windows.Forms.Padding(2);
            this.distanceChart.Name = "distanceChart";
            this.distanceChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.distanceChart.Series.Add(series2);
            this.distanceChart.Size = new System.Drawing.Size(486, 273);
            this.distanceChart.TabIndex = 8;
            this.distanceChart.Text = "distanceChart";
            // 
            // KeyGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(524, 381);
            this.Controls.Add(this.tabPanel);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.keySizeBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.keyWarningLabel);
            this.Controls.Add(this.keySizeLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "KeyGenForm";
            this.Text = "KeyGenForm";
            ((System.ComponentModel.ISupportInitialize)(this.keySizeBox)).EndInit();
            this.tabPanel.ResumeLayout(false);
            this.sizeDiffTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).EndInit();
            this.distanceTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.distanceChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label keySizeLabel;
        private System.Windows.Forms.Label keyWarningLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.NumericUpDown keySizeBox;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.TabControl tabPanel;
        private System.Windows.Forms.TabPage sizeDiffTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart dataChart;
        private System.Windows.Forms.TabPage distanceTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart distanceChart;
    }
}