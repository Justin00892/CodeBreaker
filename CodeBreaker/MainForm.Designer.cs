namespace CodeBreaker
{
    partial class MainForm
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
            this.textArea = new System.Windows.Forms.TextBox();
            this.encryptButton = new System.Windows.Forms.Button();
            this.decryptButton = new System.Windows.Forms.Button();
            this.uploadButton = new System.Windows.Forms.Button();
            this.uploadFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.generateButton = new System.Windows.Forms.Button();
            this.attackButton = new System.Windows.Forms.Button();
            this.privateKeyLabel = new System.Windows.Forms.Label();
            this.publicKeyLabel = new System.Windows.Forms.Label();
            this.modLabel = new System.Windows.Forms.Label();
            this.publicKeyTextBox = new System.Windows.Forms.TextBox();
            this.nTextBox = new System.Windows.Forms.TextBox();
            this.privateKeyTextBox = new System.Windows.Forms.TextBox();
            this.enterKeysButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keyGenAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textArea
            // 
            this.textArea.Location = new System.Drawing.Point(12, 27);
            this.textArea.Multiline = true;
            this.textArea.Name = "textArea";
            this.textArea.ReadOnly = true;
            this.textArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textArea.Size = new System.Drawing.Size(286, 425);
            this.textArea.TabIndex = 0;
            // 
            // encryptButton
            // 
            this.encryptButton.Enabled = false;
            this.encryptButton.Location = new System.Drawing.Point(305, 326);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(114, 38);
            this.encryptButton.TabIndex = 1;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // decryptButton
            // 
            this.decryptButton.Enabled = false;
            this.decryptButton.Location = new System.Drawing.Point(305, 370);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(114, 38);
            this.decryptButton.TabIndex = 2;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // uploadButton
            // 
            this.uploadButton.Location = new System.Drawing.Point(305, 27);
            this.uploadButton.Name = "uploadButton";
            this.uploadButton.Size = new System.Drawing.Size(114, 38);
            this.uploadButton.TabIndex = 3;
            this.uploadButton.Text = "Upload";
            this.uploadButton.UseVisualStyleBackColor = true;
            this.uploadButton.Click += new System.EventHandler(this.UploadButton_Click);
            // 
            // generateButton
            // 
            this.generateButton.Enabled = false;
            this.generateButton.Location = new System.Drawing.Point(306, 71);
            this.generateButton.Name = "generateButton";
            this.generateButton.Size = new System.Drawing.Size(114, 38);
            this.generateButton.TabIndex = 4;
            this.generateButton.Text = "Generate Keys";
            this.generateButton.UseVisualStyleBackColor = true;
            this.generateButton.Click += new System.EventHandler(this.GenerateButton_Click);
            // 
            // attackButton
            // 
            this.attackButton.Enabled = false;
            this.attackButton.Location = new System.Drawing.Point(305, 414);
            this.attackButton.Name = "attackButton";
            this.attackButton.Size = new System.Drawing.Size(114, 38);
            this.attackButton.TabIndex = 5;
            this.attackButton.Text = "Attack";
            this.attackButton.UseVisualStyleBackColor = true;
            this.attackButton.Click += new System.EventHandler(this.AttackButton_Click);
            // 
            // privateKeyLabel
            // 
            this.privateKeyLabel.AutoSize = true;
            this.privateKeyLabel.ForeColor = System.Drawing.Color.Red;
            this.privateKeyLabel.Location = new System.Drawing.Point(304, 202);
            this.privateKeyLabel.Name = "privateKeyLabel";
            this.privateKeyLabel.Size = new System.Drawing.Size(97, 17);
            this.privateKeyLabel.TabIndex = 6;
            this.privateKeyLabel.Text = "Private Key: X";
            // 
            // publicKeyLabel
            // 
            this.publicKeyLabel.AutoSize = true;
            this.publicKeyLabel.ForeColor = System.Drawing.Color.Green;
            this.publicKeyLabel.Location = new System.Drawing.Point(304, 112);
            this.publicKeyLabel.Name = "publicKeyLabel";
            this.publicKeyLabel.Size = new System.Drawing.Size(78, 17);
            this.publicKeyLabel.TabIndex = 7;
            this.publicKeyLabel.Text = "Public Key:";
            // 
            // modLabel
            // 
            this.modLabel.AutoSize = true;
            this.modLabel.ForeColor = System.Drawing.Color.Green;
            this.modLabel.Location = new System.Drawing.Point(304, 157);
            this.modLabel.Name = "modLabel";
            this.modLabel.Size = new System.Drawing.Size(78, 17);
            this.modLabel.TabIndex = 8;
            this.modLabel.Text = "Modulus: X";
            // 
            // publicKeyTextBox
            // 
            this.publicKeyTextBox.Location = new System.Drawing.Point(304, 132);
            this.publicKeyTextBox.Name = "publicKeyTextBox";
            this.publicKeyTextBox.Size = new System.Drawing.Size(114, 22);
            this.publicKeyTextBox.TabIndex = 9;
            // 
            // nTextBox
            // 
            this.nTextBox.Location = new System.Drawing.Point(304, 177);
            this.nTextBox.Name = "nTextBox";
            this.nTextBox.Size = new System.Drawing.Size(114, 22);
            this.nTextBox.TabIndex = 10;
            // 
            // privateKeyTextBox
            // 
            this.privateKeyTextBox.Location = new System.Drawing.Point(304, 222);
            this.privateKeyTextBox.Name = "privateKeyTextBox";
            this.privateKeyTextBox.Size = new System.Drawing.Size(114, 22);
            this.privateKeyTextBox.TabIndex = 11;
            // 
            // enterKeysButton
            // 
            this.enterKeysButton.Location = new System.Drawing.Point(304, 250);
            this.enterKeysButton.Name = "enterKeysButton";
            this.enterKeysButton.Size = new System.Drawing.Size(114, 38);
            this.enterKeysButton.TabIndex = 12;
            this.enterKeysButton.Text = "Enter Keys";
            this.enterKeysButton.UseVisualStyleBackColor = true;
            this.enterKeysButton.Click += new System.EventHandler(this.EnterKeysButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(432, 28);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // statsToolStripMenuItem
            // 
            this.statsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keyGenAnalysisToolStripMenuItem});
            this.statsToolStripMenuItem.Name = "statsToolStripMenuItem";
            this.statsToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.statsToolStripMenuItem.Text = "Stats";
            // 
            // keyGenAnalysisToolStripMenuItem
            // 
            this.keyGenAnalysisToolStripMenuItem.Name = "keyGenAnalysisToolStripMenuItem";
            this.keyGenAnalysisToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.keyGenAnalysisToolStripMenuItem.Text = "KeyGen Analysis";
            this.keyGenAnalysisToolStripMenuItem.Click += new System.EventHandler(this.KeyGenAnalysisToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 462);
            this.Controls.Add(this.enterKeysButton);
            this.Controls.Add(this.privateKeyTextBox);
            this.Controls.Add(this.nTextBox);
            this.Controls.Add(this.publicKeyTextBox);
            this.Controls.Add(this.modLabel);
            this.Controls.Add(this.publicKeyLabel);
            this.Controls.Add(this.privateKeyLabel);
            this.Controls.Add(this.attackButton);
            this.Controls.Add(this.generateButton);
            this.Controls.Add(this.uploadButton);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.textArea);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textArea;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Button uploadButton;
        private System.Windows.Forms.OpenFileDialog uploadFileDialog;
        private System.Windows.Forms.Button generateButton;
        private System.Windows.Forms.Button attackButton;
        private System.Windows.Forms.Label privateKeyLabel;
        private System.Windows.Forms.Label publicKeyLabel;
        private System.Windows.Forms.Label modLabel;
        private System.Windows.Forms.TextBox publicKeyTextBox;
        private System.Windows.Forms.TextBox nTextBox;
        private System.Windows.Forms.TextBox privateKeyTextBox;
        private System.Windows.Forms.Button enterKeysButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem statsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem keyGenAnalysisToolStripMenuItem;
    }
}

