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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.keySizeLabel = new System.Windows.Forms.Label();
            this.keyWarningLabel = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.keySizeBox = new System.Windows.Forms.NumericUpDown();
            this.dataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.rangeLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.keySizeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).BeginInit();
            this.SuspendLayout();
            // 
            // keySizeLabel
            // 
            this.keySizeLabel.AutoSize = true;
            this.keySizeLabel.Location = new System.Drawing.Point(13, 13);
            this.keySizeLabel.Name = "keySizeLabel";
            this.keySizeLabel.Size = new System.Drawing.Size(92, 17);
            this.keySizeLabel.TabIndex = 0;
            this.keySizeLabel.Text = "Max Key Size";
            // 
            // keyWarningLabel
            // 
            this.keyWarningLabel.AutoSize = true;
            this.keyWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.keyWarningLabel.Location = new System.Drawing.Point(169, 13);
            this.keyWarningLabel.Name = "keyWarningLabel";
            this.keyWarningLabel.Size = new System.Drawing.Size(0, 17);
            this.keyWarningLabel.TabIndex = 2;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(13, 34);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(150, 23);
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
            this.keySizeBox.Location = new System.Drawing.Point(111, 11);
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
            this.keySizeBox.Size = new System.Drawing.Size(52, 22);
            this.keySizeBox.TabIndex = 5;
            this.keySizeBox.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // dataChart
            // 
            chartArea2.AxisX.Title = "Modulus & Totient Size (# Digits)";
            chartArea2.AxisY.Maximum = 100D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.Title = "Common Significant Digits";
            chartArea2.AxisY2.IsMarginVisible = false;
            chartArea2.Name = "ChartArea1";
            this.dataChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.dataChart.Legends.Add(legend2);
            this.dataChart.Location = new System.Drawing.Point(12, 63);
            this.dataChart.Name = "dataChart";
            this.dataChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.dataChart.Series.Add(series2);
            this.dataChart.Size = new System.Drawing.Size(675, 375);
            this.dataChart.TabIndex = 6;
            this.dataChart.Text = "dataChart";
            // 
            // rangeLabel
            // 
            this.rangeLabel.AutoSize = true;
            this.rangeLabel.Location = new System.Drawing.Point(12, 443);
            this.rangeLabel.Name = "rangeLabel";
            this.rangeLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rangeLabel.Size = new System.Drawing.Size(115, 17);
            this.rangeLabel.TabIndex = 7;
            this.rangeLabel.Text = "Average Range: ";
            this.rangeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // KeyGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 469);
            this.Controls.Add(this.rangeLabel);
            this.Controls.Add(this.dataChart);
            this.Controls.Add(this.keySizeBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.keyWarningLabel);
            this.Controls.Add(this.keySizeLabel);
            this.Name = "KeyGenForm";
            this.Text = "KeyGenForm";
            ((System.ComponentModel.ISupportInitialize)(this.keySizeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label keySizeLabel;
        private System.Windows.Forms.Label keyWarningLabel;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.NumericUpDown keySizeBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart dataChart;
        private System.Windows.Forms.Label rangeLabel;
    }
}