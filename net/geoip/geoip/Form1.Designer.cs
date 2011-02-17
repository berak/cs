namespace geoip
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            pickle();

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ipCombo = new System.Windows.Forms.ToolStripComboBox();
            this.findButton = new System.Windows.Forms.ToolStripButton();
            this.zoomBox = new System.Windows.Forms.ToolStripComboBox();
            this.labelL = new System.Windows.Forms.ToolStripButton();
            this.labelU = new System.Windows.Forms.ToolStripButton();
            this.labelD = new System.Windows.Forms.ToolStripButton();
            this.labelR = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ipCombo,
            this.findButton,
            this.zoomBox,
            this.labelL,
            this.labelU,
            this.labelD,
            this.labelR});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(360, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ipCombo
            // 
            this.ipCombo.Name = "ipCombo";
            this.ipCombo.Size = new System.Drawing.Size(181, 25);
            this.ipCombo.Text = "88.4.5.6";
            this.ipCombo.ToolTipText = "ip, host,  latlon or place name";
            // 
            // findButton
            // 
            this.findButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.findButton.Image = ((System.Drawing.Image)(resources.GetObject("findButton.Image")));
            this.findButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findButton.Name = "findButton";
            this.findButton.Size = new System.Drawing.Size(40, 22);
            this.findButton.Text = "locate";
            this.findButton.Click += new System.EventHandler(this.findButton_Click);
            // 
            // zoomBox
            // 
            this.zoomBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.zoomBox.AutoSize = false;
            this.zoomBox.DropDownWidth = 45;
            this.zoomBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17"});
            this.zoomBox.Name = "zoomBox";
            this.zoomBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.zoomBox.Size = new System.Drawing.Size(45, 21);
            this.zoomBox.Text = "11";
            this.zoomBox.ToolTipText = "zoom level";
            // 
            // labelL
            // 
            this.labelL.AutoSize = false;
            this.labelL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelL.Image = ((System.Drawing.Image)(resources.GetObject("labelL.Image")));
            this.labelL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.labelL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.labelL.Name = "labelL";
            this.labelL.Size = new System.Drawing.Size(18, 22);
            this.labelL.Text = "<";
            this.labelL.ToolTipText = "west";
            this.labelL.Click += new System.EventHandler(this.labelL_Click);
            // 
            // labelU
            // 
            this.labelU.AutoSize = false;
            this.labelU.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelU.Image = ((System.Drawing.Image)(resources.GetObject("labelU.Image")));
            this.labelU.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.labelU.Name = "labelU";
            this.labelU.Size = new System.Drawing.Size(18, 22);
            this.labelU.Text = "^";
            this.labelU.ToolTipText = "north";
            this.labelU.Click += new System.EventHandler(this.labelU_Click);
            // 
            // labelD
            // 
            this.labelD.AutoSize = false;
            this.labelD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelD.Image = ((System.Drawing.Image)(resources.GetObject("labelD.Image")));
            this.labelD.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.labelD.Name = "labelD";
            this.labelD.Size = new System.Drawing.Size(18, 22);
            this.labelD.Text = "v";
            this.labelD.ToolTipText = "south";
            this.labelD.Click += new System.EventHandler(this.labelD_Click);
            // 
            // labelR
            // 
            this.labelR.AutoSize = false;
            this.labelR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelR.Image = ((System.Drawing.Image)(resources.GetObject("labelR.Image")));
            this.labelR.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(18, 17);
            this.labelR.Text = ">";
            this.labelR.ToolTipText = "east";
            this.labelR.Click += new System.EventHandler(this.labelR_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(360, 333);
            this.splitContainer1.SplitterDistance = 78;
            this.splitContainer1.TabIndex = 3;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(360, 78);
            this.textBox.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 251);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 358);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "geolocator";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox ipCombo;
        private System.Windows.Forms.ToolStripButton findButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripComboBox zoomBox;
        private System.Windows.Forms.ToolStripButton labelR;
        private System.Windows.Forms.ToolStripButton labelL;
        private System.Windows.Forms.ToolStripButton labelU;
        private System.Windows.Forms.ToolStripButton labelD;
    }
}

