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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyGenForm));
            this.keySizeLabel = new System.Windows.Forms.Label();
            this.keyWarningLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.keySizeBox = new System.Windows.Forms.NumericUpDown();
            this.testButton = new System.Windows.Forms.Button();
            this.distanceTab = new System.Windows.Forms.TabPage();
            this.distanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sizeDiffTab = new System.Windows.Forms.TabPage();
            this.dataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPanel = new System.Windows.Forms.TabControl();
            this.nTab = new System.Windows.Forms.TabPage();
            this.nChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.loadingIcon = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.keySizeBox)).BeginInit();
            this.distanceTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.distanceChart)).BeginInit();
            this.sizeDiffTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).BeginInit();
            this.tabPanel.SuspendLayout();
            this.nTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nChart)).BeginInit();
            this.loadingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIcon)).BeginInit();
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
            // distanceTab
            // 
            this.distanceTab.Controls.Add(this.distanceChart);
            this.distanceTab.Location = new System.Drawing.Point(4, 22);
            this.distanceTab.Name = "distanceTab";
            this.distanceTab.Padding = new System.Windows.Forms.Padding(3);
            this.distanceTab.Size = new System.Drawing.Size(492, 279);
            this.distanceTab.TabIndex = 1;
            this.distanceTab.Text = "Differance";
            this.distanceTab.UseVisualStyleBackColor = true;
            // 
            // distanceChart
            // 
            chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea1.AxisX.Title = "Size (bits)";
            chartArea1.AxisY.IsLogarithmic = true;
            chartArea1.AxisY.MajorTickMark.Interval = 0D;
            chartArea1.AxisY.MajorTickMark.IntervalOffset = 0D;
            chartArea1.AxisY.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.Title = "N Minus Totient";
            chartArea1.AxisY2.IsMarginVisible = false;
            chartArea1.Name = "ChartArea1";
            this.distanceChart.ChartAreas.Add(chartArea1);
            this.distanceChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.distanceChart.Legends.Add(legend1);
            this.distanceChart.Location = new System.Drawing.Point(3, 3);
            this.distanceChart.Margin = new System.Windows.Forms.Padding(2);
            this.distanceChart.Name = "distanceChart";
            this.distanceChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.distanceChart.Series.Add(series1);
            this.distanceChart.Size = new System.Drawing.Size(486, 273);
            this.distanceChart.TabIndex = 8;
            this.distanceChart.Text = "distanceChart";
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
            chartArea2.AxisX.Title = "Modulus & Totient Size (bits)";
            chartArea2.AxisY.Maximum = 100D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.Title = "Common Significant Digits";
            chartArea2.AxisY2.IsMarginVisible = false;
            chartArea2.Name = "ChartArea1";
            this.dataChart.ChartAreas.Add(chartArea2);
            this.dataChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.dataChart.Legends.Add(legend2);
            this.dataChart.Location = new System.Drawing.Point(3, 3);
            this.dataChart.Margin = new System.Windows.Forms.Padding(2);
            this.dataChart.Name = "dataChart";
            this.dataChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.dataChart.Series.Add(series2);
            this.dataChart.Size = new System.Drawing.Size(486, 273);
            this.dataChart.TabIndex = 7;
            this.dataChart.Text = "dataChart";
            // 
            // tabPanel
            // 
            this.tabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPanel.Controls.Add(this.sizeDiffTab);
            this.tabPanel.Controls.Add(this.distanceTab);
            this.tabPanel.Controls.Add(this.nTab);
            this.tabPanel.Location = new System.Drawing.Point(12, 52);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(500, 305);
            this.tabPanel.TabIndex = 9;
            // 
            // nTab
            // 
            this.nTab.Controls.Add(this.nChart);
            this.nTab.Location = new System.Drawing.Point(4, 22);
            this.nTab.Name = "nTab";
            this.nTab.Size = new System.Drawing.Size(492, 279);
            this.nTab.TabIndex = 2;
            this.nTab.Text = "N vs Totient";
            this.nTab.UseVisualStyleBackColor = true;
            // 
            // nChart
            // 
            chartArea3.AxisX.IsLogarithmic = true;
            chartArea3.AxisX.Minimum = 1E+55D;
            chartArea3.AxisX.Title = "N";
            chartArea3.AxisY.IsLogarithmic = true;
            chartArea3.AxisY.MajorTickMark.Interval = 0D;
            chartArea3.AxisY.MajorTickMark.IntervalOffset = 0D;
            chartArea3.AxisY.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea3.AxisY.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea3.AxisY.Minimum = 1E+55D;
            chartArea3.AxisY.ScaleBreakStyle.Enabled = true;
            chartArea3.AxisY.Title = "Totient";
            chartArea3.AxisY2.IsMarginVisible = false;
            chartArea3.Name = "ChartArea1";
            this.nChart.ChartAreas.Add(chartArea3);
            this.nChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.nChart.Legends.Add(legend3);
            this.nChart.Location = new System.Drawing.Point(0, 0);
            this.nChart.Margin = new System.Windows.Forms.Padding(2);
            this.nChart.Name = "nChart";
            this.nChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.nChart.Series.Add(series3);
            this.nChart.Size = new System.Drawing.Size(492, 279);
            this.nChart.TabIndex = 9;
            this.nChart.Text = "nChart";
            // 
            // loadingPanel
            // 
            this.loadingPanel.Controls.Add(this.loadingIcon);
            this.loadingPanel.Location = new System.Drawing.Point(12, 52);
            this.loadingPanel.Name = "loadingPanel";
            this.loadingPanel.Size = new System.Drawing.Size(500, 317);
            this.loadingPanel.TabIndex = 10;
            this.loadingPanel.Visible = false;
            // 
            // loadingIcon
            // 
            this.loadingIcon.Image = ((System.Drawing.Image)(resources.GetObject("loadingIcon.Image")));
            this.loadingIcon.Location = new System.Drawing.Point(3, 3);
            this.loadingIcon.Name = "loadingIcon";
            this.loadingIcon.Size = new System.Drawing.Size(493, 311);
            this.loadingIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loadingIcon.TabIndex = 0;
            this.loadingIcon.TabStop = false;
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
            this.Controls.Add(this.loadingPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "KeyGenForm";
            this.Text = "KeyGenForm";
            ((System.ComponentModel.ISupportInitialize)(this.keySizeBox)).EndInit();
            this.distanceTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.distanceChart)).EndInit();
            this.sizeDiffTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).EndInit();
            this.tabPanel.ResumeLayout(false);
            this.nTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nChart)).EndInit();
            this.loadingPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label keySizeLabel;
        private System.Windows.Forms.Label keyWarningLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.NumericUpDown keySizeBox;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.TabPage distanceTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart distanceChart;
        private System.Windows.Forms.TabPage sizeDiffTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart dataChart;
        private System.Windows.Forms.TabControl tabPanel;
        private System.Windows.Forms.TabPage nTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart nChart;
        private System.Windows.Forms.Panel loadingPanel;
        private System.Windows.Forms.PictureBox loadingIcon;
    }
}