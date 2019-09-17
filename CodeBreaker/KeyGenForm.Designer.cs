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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeyGenForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.minKeySizeLabel = new System.Windows.Forms.Label();
            this.keyWarningLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.maxKeySizeBox = new System.Windows.Forms.NumericUpDown();
            this.testButton = new System.Windows.Forms.Button();
            this.loadingPanel = new System.Windows.Forms.Panel();
            this.loadingIcon = new System.Windows.Forms.PictureBox();
            this.repLabel = new System.Windows.Forms.Label();
            this.replicatesBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.minKeySizeBox = new System.Windows.Forms.NumericUpDown();
            this.distanceTab = new System.Windows.Forms.TabPage();
            this.distanceChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.nTab = new System.Windows.Forms.TabPage();
            this.versusChart = new LiveCharts.WinForms.CartesianChart();
            this.sizeTab = new System.Windows.Forms.TabPage();
            this.sizeChart = new LiveCharts.WinForms.CartesianChart();
            this.tabPanel = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.maxKeySizeBox)).BeginInit();
            this.loadingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loadingIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.replicatesBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minKeySizeBox)).BeginInit();
            this.distanceTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.distanceChart)).BeginInit();
            this.nTab.SuspendLayout();
            this.sizeTab.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // minKeySizeLabel
            // 
            this.minKeySizeLabel.AutoSize = true;
            this.minKeySizeLabel.Location = new System.Drawing.Point(10, 11);
            this.minKeySizeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.minKeySizeLabel.Name = "minKeySizeLabel";
            this.minKeySizeLabel.Size = new System.Drawing.Size(68, 13);
            this.minKeySizeLabel.TabIndex = 0;
            this.minKeySizeLabel.Text = "Min Key Size";
            // 
            // keyWarningLabel
            // 
            this.keyWarningLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.keyWarningLabel.AutoSize = true;
            this.keyWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.keyWarningLabel.Location = new System.Drawing.Point(397, 11);
            this.keyWarningLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.keyWarningLabel.Name = "keyWarningLabel";
            this.keyWarningLabel.Size = new System.Drawing.Size(0, 13);
            this.keyWarningLabel.TabIndex = 2;
            this.keyWarningLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // runButton
            // 
            this.runButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.runButton.Location = new System.Drawing.Point(401, 8);
            this.runButton.Margin = new System.Windows.Forms.Padding(2);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(112, 19);
            this.runButton.TabIndex = 3;
            this.runButton.Text = "Run Analysis";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // maxKeySizeBox
            // 
            this.maxKeySizeBox.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.maxKeySizeBox.Location = new System.Drawing.Point(203, 9);
            this.maxKeySizeBox.Margin = new System.Windows.Forms.Padding(2);
            this.maxKeySizeBox.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.maxKeySizeBox.Minimum = new decimal(new int[] {
            384,
            0,
            0,
            0});
            this.maxKeySizeBox.Name = "maxKeySizeBox";
            this.maxKeySizeBox.Size = new System.Drawing.Size(39, 20);
            this.maxKeySizeBox.TabIndex = 5;
            this.maxKeySizeBox.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // testButton
            // 
            this.testButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.testButton.Location = new System.Drawing.Point(457, 28);
            this.testButton.Margin = new System.Windows.Forms.Padding(2);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(56, 19);
            this.testButton.TabIndex = 8;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.TestButton_Click);
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
            // repLabel
            // 
            this.repLabel.AutoSize = true;
            this.repLabel.Location = new System.Drawing.Point(10, 33);
            this.repLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.repLabel.Name = "repLabel";
            this.repLabel.Size = new System.Drawing.Size(57, 13);
            this.repLabel.TabIndex = 11;
            this.repLabel.Text = "Replicates";
            // 
            // replicatesBox
            // 
            this.replicatesBox.Location = new System.Drawing.Point(83, 31);
            this.replicatesBox.Margin = new System.Windows.Forms.Padding(2);
            this.replicatesBox.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.replicatesBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.replicatesBox.Name = "replicatesBox";
            this.replicatesBox.Size = new System.Drawing.Size(39, 20);
            this.replicatesBox.TabIndex = 12;
            this.replicatesBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Max Key Size";
            // 
            // minKeySizeBox
            // 
            this.minKeySizeBox.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.minKeySizeBox.Location = new System.Drawing.Point(85, 9);
            this.minKeySizeBox.Margin = new System.Windows.Forms.Padding(2);
            this.minKeySizeBox.Maximum = new decimal(new int[] {
            16384,
            0,
            0,
            0});
            this.minKeySizeBox.Minimum = new decimal(new int[] {
            384,
            0,
            0,
            0});
            this.minKeySizeBox.Name = "minKeySizeBox";
            this.minKeySizeBox.Size = new System.Drawing.Size(39, 20);
            this.minKeySizeBox.TabIndex = 14;
            this.minKeySizeBox.Value = new decimal(new int[] {
            384,
            0,
            0,
            0});
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
            chartArea3.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea3.AxisX.Title = "Size (bits)";
            chartArea3.AxisY.IsLogarithmic = true;
            chartArea3.AxisY.MajorTickMark.Interval = 0D;
            chartArea3.AxisY.MajorTickMark.IntervalOffset = 0D;
            chartArea3.AxisY.MajorTickMark.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea3.AxisY.MajorTickMark.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea3.AxisY.Title = "N Minus Totient";
            chartArea3.AxisY2.IsMarginVisible = false;
            chartArea3.Name = "ChartArea1";
            this.distanceChart.ChartAreas.Add(chartArea3);
            this.distanceChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.distanceChart.Legends.Add(legend3);
            this.distanceChart.Location = new System.Drawing.Point(3, 3);
            this.distanceChart.Margin = new System.Windows.Forms.Padding(2);
            this.distanceChart.Name = "distanceChart";
            this.distanceChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.distanceChart.Series.Add(series3);
            this.distanceChart.Size = new System.Drawing.Size(486, 273);
            this.distanceChart.TabIndex = 8;
            this.distanceChart.Text = "distanceChart";
            // 
            // nTab
            // 
            this.nTab.Controls.Add(this.versusChart);
            this.nTab.Location = new System.Drawing.Point(4, 22);
            this.nTab.Name = "nTab";
            this.nTab.Size = new System.Drawing.Size(492, 279);
            this.nTab.TabIndex = 4;
            this.nTab.Text = "N vs Totient (Log Scale)";
            this.nTab.UseVisualStyleBackColor = true;
            // 
            // versusChart
            // 
            this.versusChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versusChart.Location = new System.Drawing.Point(0, 0);
            this.versusChart.Name = "versusChart";
            this.versusChart.Size = new System.Drawing.Size(492, 279);
            this.versusChart.TabIndex = 1;
            this.versusChart.Text = "Size Chart";
            // 
            // sizeTab
            // 
            this.sizeTab.Controls.Add(this.sizeChart);
            this.sizeTab.Location = new System.Drawing.Point(4, 22);
            this.sizeTab.Name = "sizeTab";
            this.sizeTab.Size = new System.Drawing.Size(492, 279);
            this.sizeTab.TabIndex = 3;
            this.sizeTab.Text = "Size";
            this.sizeTab.UseVisualStyleBackColor = true;
            // 
            // sizeChart
            // 
            this.sizeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeChart.Location = new System.Drawing.Point(0, 0);
            this.sizeChart.Name = "sizeChart";
            this.sizeChart.Size = new System.Drawing.Size(492, 279);
            this.sizeChart.TabIndex = 0;
            this.sizeChart.Text = "Size Chart";
            // 
            // tabPanel
            // 
            this.tabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPanel.Controls.Add(this.sizeTab);
            this.tabPanel.Controls.Add(this.nTab);
            this.tabPanel.Controls.Add(this.distanceTab);
            this.tabPanel.Location = new System.Drawing.Point(12, 52);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(500, 305);
            this.tabPanel.TabIndex = 9;
            // 
            // KeyGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(524, 381);
            this.Controls.Add(this.minKeySizeBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.replicatesBox);
            this.Controls.Add(this.repLabel);
            this.Controls.Add(this.tabPanel);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.maxKeySizeBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.keyWarningLabel);
            this.Controls.Add(this.minKeySizeLabel);
            this.Controls.Add(this.loadingPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "KeyGenForm";
            this.Text = "KeyGenForm";
            ((System.ComponentModel.ISupportInitialize)(this.maxKeySizeBox)).EndInit();
            this.loadingPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.loadingIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.replicatesBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minKeySizeBox)).EndInit();
            this.distanceTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.distanceChart)).EndInit();
            this.nTab.ResumeLayout(false);
            this.sizeTab.ResumeLayout(false);
            this.tabPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label minKeySizeLabel;
        private System.Windows.Forms.Label keyWarningLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.NumericUpDown maxKeySizeBox;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Panel loadingPanel;
        private System.Windows.Forms.PictureBox loadingIcon;
        private System.Windows.Forms.Label repLabel;
        private System.Windows.Forms.NumericUpDown replicatesBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown minKeySizeBox;
        private System.Windows.Forms.TabPage distanceTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart distanceChart;
        private System.Windows.Forms.TabPage nTab;
        private LiveCharts.WinForms.CartesianChart versusChart;
        private System.Windows.Forms.TabPage sizeTab;
        private LiveCharts.WinForms.CartesianChart sizeChart;
        private System.Windows.Forms.TabControl tabPanel;
    }
}