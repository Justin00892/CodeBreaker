﻿namespace CodeBreaker
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
            this.rangeLabel = new System.Windows.Forms.Label();
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
            this.keySizeLabel.Location = new System.Drawing.Point(13, 14);
            this.keySizeLabel.Name = "keySizeLabel";
            this.keySizeLabel.Size = new System.Drawing.Size(92, 17);
            this.keySizeLabel.TabIndex = 0;
            this.keySizeLabel.Text = "Max Key Size";
            // 
            // keyWarningLabel
            // 
            this.keyWarningLabel.AutoSize = true;
            this.keyWarningLabel.ForeColor = System.Drawing.Color.Red;
            this.keyWarningLabel.Location = new System.Drawing.Point(169, 14);
            this.keyWarningLabel.Name = "keyWarningLabel";
            this.keyWarningLabel.Size = new System.Drawing.Size(0, 17);
            this.keyWarningLabel.TabIndex = 2;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(13, 34);
            this.runButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(149, 23);
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
            this.keySizeBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(612, 11);
            this.testButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 8;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // tabPanel
            // 
            this.tabPanel.Controls.Add(this.sizeDiffTab);
            this.tabPanel.Controls.Add(this.distanceTab);
            this.tabPanel.Location = new System.Drawing.Point(16, 64);
            this.tabPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(667, 375);
            this.tabPanel.TabIndex = 9;
            // 
            // sizeDiffTab
            // 
            this.sizeDiffTab.Controls.Add(this.dataChart);
            this.sizeDiffTab.Location = new System.Drawing.Point(4, 25);
            this.sizeDiffTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sizeDiffTab.Name = "sizeDiffTab";
            this.sizeDiffTab.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sizeDiffTab.Size = new System.Drawing.Size(659, 346);
            this.sizeDiffTab.TabIndex = 0;
            this.sizeDiffTab.Text = "Size";
            this.sizeDiffTab.UseVisualStyleBackColor = true;
            // 
            // dataChart
            // 
            chartArea1.AxisX.Title = "Modulus & Totient Size (# Digits)";
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.Title = "Common Significant Digits";
            chartArea1.AxisY2.IsMarginVisible = false;
            chartArea1.Name = "ChartArea1";
            this.dataChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.dataChart.Legends.Add(legend1);
            this.dataChart.Location = new System.Drawing.Point(7, 6);
            this.dataChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataChart.Name = "dataChart";
            this.dataChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.dataChart.Series.Add(series1);
            this.dataChart.Size = new System.Drawing.Size(643, 331);
            this.dataChart.TabIndex = 7;
            this.dataChart.Text = "dataChart";
            // 
            // distanceTab
            // 
            this.distanceTab.Controls.Add(this.distanceChart);
            this.distanceTab.Location = new System.Drawing.Point(4, 25);
            this.distanceTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.distanceTab.Name = "distanceTab";
            this.distanceTab.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.distanceTab.Size = new System.Drawing.Size(659, 346);
            this.distanceTab.TabIndex = 1;
            this.distanceTab.Text = "Distance";
            this.distanceTab.UseVisualStyleBackColor = true;
            // 
            // distanceChart
            // 
            chartArea2.AxisX.Title = "Modulus & Totient Size (# Digits)";
            chartArea2.AxisY.Title = "Distance From Midpoint (E55)";
            chartArea2.AxisY2.IsMarginVisible = false;
            chartArea2.Name = "ChartArea1";
            this.distanceChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.distanceChart.Legends.Add(legend2);
            this.distanceChart.Location = new System.Drawing.Point(3, 6);
            this.distanceChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.distanceChart.Name = "distanceChart";
            this.distanceChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.distanceChart.Series.Add(series2);
            this.distanceChart.Size = new System.Drawing.Size(643, 331);
            this.distanceChart.TabIndex = 8;
            this.distanceChart.Text = "distanceChart";
            // 
            // KeyGenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 469);
            this.Controls.Add(this.tabPanel);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.rangeLabel);
            this.Controls.Add(this.keySizeBox);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.keyWarningLabel);
            this.Controls.Add(this.keySizeLabel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Label rangeLabel;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.TabControl tabPanel;
        private System.Windows.Forms.TabPage sizeDiffTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart dataChart;
        private System.Windows.Forms.TabPage distanceTab;
        private System.Windows.Forms.DataVisualization.Charting.Chart distanceChart;
    }
}