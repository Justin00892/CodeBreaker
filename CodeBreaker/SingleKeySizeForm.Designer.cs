namespace CodeBreaker
{
    partial class SingleKeySizeForm
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
            this.controlPanel = new System.Windows.Forms.Panel();
            this.continuousButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.graphTabPanel = new System.Windows.Forms.TabControl();
            this.versusTab = new System.Windows.Forms.TabPage();
            this.versusChart = new LiveCharts.WinForms.CartesianChart();
            this.sizeTab = new System.Windows.Forms.TabPage();
            this.sizeChart = new LiveCharts.WinForms.CartesianChart();
            this.controlPanel.SuspendLayout();
            this.graphTabPanel.SuspendLayout();
            this.versusTab.SuspendLayout();
            this.sizeTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // controlPanel
            // 
            this.controlPanel.Controls.Add(this.continuousButton);
            this.controlPanel.Controls.Add(this.addButton);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(1067, 39);
            this.controlPanel.TabIndex = 4;
            // 
            // continuousButton
            // 
            this.continuousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.continuousButton.BackColor = System.Drawing.Color.Lime;
            this.continuousButton.Location = new System.Drawing.Point(915, 4);
            this.continuousButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.continuousButton.Name = "continuousButton";
            this.continuousButton.Size = new System.Drawing.Size(136, 28);
            this.continuousButton.TabIndex = 5;
            this.continuousButton.Text = "Run Continuously";
            this.continuousButton.UseVisualStyleBackColor = false;
            this.continuousButton.Click += new System.EventHandler(this.ContinuousButton_Click);
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(797, 4);
            this.addButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(109, 28);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add More";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // graphTabPanel
            // 
            this.graphTabPanel.Controls.Add(this.versusTab);
            this.graphTabPanel.Controls.Add(this.sizeTab);
            this.graphTabPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphTabPanel.Location = new System.Drawing.Point(0, 39);
            this.graphTabPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.graphTabPanel.Name = "graphTabPanel";
            this.graphTabPanel.SelectedIndex = 0;
            this.graphTabPanel.Size = new System.Drawing.Size(1067, 580);
            this.graphTabPanel.TabIndex = 5;
            // 
            // versusTab
            // 
            this.versusTab.Controls.Add(this.versusChart);
            this.versusTab.Location = new System.Drawing.Point(4, 25);
            this.versusTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.versusTab.Name = "versusTab";
            this.versusTab.Size = new System.Drawing.Size(1059, 551);
            this.versusTab.TabIndex = 0;
            this.versusTab.Text = "N vs Totient";
            this.versusTab.UseVisualStyleBackColor = true;
            // 
            // versusChart
            // 
            this.versusChart.BackColor = System.Drawing.Color.White;
            this.versusChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versusChart.Location = new System.Drawing.Point(0, 0);
            this.versusChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.versusChart.Name = "versusChart";
            this.versusChart.Size = new System.Drawing.Size(1059, 551);
            this.versusChart.TabIndex = 4;
            this.versusChart.Text = "Versus Chart";
            // 
            // sizeTab
            // 
            this.sizeTab.Controls.Add(this.sizeChart);
            this.sizeTab.Location = new System.Drawing.Point(4, 25);
            this.sizeTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sizeTab.Name = "sizeTab";
            this.sizeTab.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sizeTab.Size = new System.Drawing.Size(1059, 551);
            this.sizeTab.TabIndex = 1;
            this.sizeTab.Text = "Size Distribution";
            this.sizeTab.UseVisualStyleBackColor = true;
            // 
            // sizeChart
            // 
            this.sizeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sizeChart.Location = new System.Drawing.Point(4, 4);
            this.sizeChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sizeChart.Name = "sizeChart";
            this.sizeChart.Size = new System.Drawing.Size(1051, 543);
            this.sizeChart.TabIndex = 0;
            this.sizeChart.Text = "cartesianChart1";
            // 
            // SingleKeySizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 619);
            this.Controls.Add(this.graphTabPanel);
            this.Controls.Add(this.controlPanel);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SingleKeySizeForm";
            this.Text = "SingleKeySizeForm";
            this.controlPanel.ResumeLayout(false);
            this.graphTabPanel.ResumeLayout(false);
            this.versusTab.ResumeLayout(false);
            this.sizeTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.TabControl graphTabPanel;
        private System.Windows.Forms.TabPage versusTab;
        private LiveCharts.WinForms.CartesianChart versusChart;
        private System.Windows.Forms.TabPage sizeTab;
        private LiveCharts.WinForms.CartesianChart sizeChart;
        private System.Windows.Forms.Button continuousButton;
    }
}