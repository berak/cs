namespace pscan
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabScanPorts = new System.Windows.Forms.TabPage();
            this.status = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.scannedPorts = new System.Windows.Forms.ListBox();
            this.tabScanIps = new System.Windows.Forms.TabPage();
            this.propertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.startIpScan = new System.Windows.Forms.Button();
            this.scannedHosts = new System.Windows.Forms.ListBox();
            this.tabPrefs = new System.Windows.Forms.TabPage();
            this.propertyGrid3 = new System.Windows.Forms.PropertyGrid();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ping1 = new System.Net.NetworkInformation.Ping();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.tabControl1.SuspendLayout();
            this.tabScanPorts.SuspendLayout();
            this.tabScanIps.SuspendLayout();
            this.tabPrefs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabScanPorts);
            this.tabControl1.Controls.Add(this.tabScanIps);
            this.tabControl1.Controls.Add(this.tabPrefs);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(336, 193);
            this.tabControl1.TabIndex = 0;
            // 
            // tabScanPorts
            // 
            this.tabScanPorts.Controls.Add(this.status);
            this.tabScanPorts.Controls.Add(this.buttonStart);
            this.tabScanPorts.Controls.Add(this.propertyGrid1);
            this.tabScanPorts.Controls.Add(this.progressBar1);
            this.tabScanPorts.Controls.Add(this.scannedPorts);
            this.tabScanPorts.Location = new System.Drawing.Point(4, 22);
            this.tabScanPorts.Name = "tabScanPorts";
            this.tabScanPorts.Size = new System.Drawing.Size(328, 167);
            this.tabScanPorts.TabIndex = 0;
            this.tabScanPorts.Text = "Scan Ports";
            this.tabScanPorts.UseVisualStyleBackColor = true;
            // 
            // status
            // 
            this.status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.status.Enabled = false;
            this.status.Location = new System.Drawing.Point(8, 99);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(176, 20);
            this.status.TabIndex = 8;
            // 
            // buttonStart
            // 
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Location = new System.Drawing.Point(8, 123);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(176, 21);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.Location = new System.Drawing.Point(8, 12);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid1.Size = new System.Drawing.Size(176, 81);
            this.propertyGrid1.TabIndex = 7;
            this.propertyGrid1.ToolbarVisible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progressBar1.Location = new System.Drawing.Point(8, 151);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(312, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 6;
            // 
            // scannedPorts
            // 
            this.scannedPorts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scannedPorts.FormattingEnabled = true;
            this.scannedPorts.Location = new System.Drawing.Point(190, 12);
            this.scannedPorts.Name = "scannedPorts";
            this.scannedPorts.Size = new System.Drawing.Size(130, 132);
            this.scannedPorts.TabIndex = 5;
            // 
            // tabScanIps
            // 
            this.tabScanIps.Controls.Add(this.propertyGrid2);
            this.tabScanIps.Controls.Add(this.progressBar2);
            this.tabScanIps.Controls.Add(this.startIpScan);
            this.tabScanIps.Controls.Add(this.scannedHosts);
            this.tabScanIps.Location = new System.Drawing.Point(4, 22);
            this.tabScanIps.Name = "tabScanIps";
            this.tabScanIps.Size = new System.Drawing.Size(328, 167);
            this.tabScanIps.TabIndex = 0;
            this.tabScanIps.Text = "Scan Ips";
            this.tabScanIps.UseVisualStyleBackColor = true;
            // 
            // propertyGrid2
            // 
            this.propertyGrid2.HelpVisible = false;
            this.propertyGrid2.Location = new System.Drawing.Point(8, 7);
            this.propertyGrid2.Name = "propertyGrid2";
            this.propertyGrid2.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid2.Size = new System.Drawing.Size(169, 103);
            this.propertyGrid2.TabIndex = 8;
            this.propertyGrid2.ToolbarVisible = false;
            // 
            // progressBar2
            // 
            this.progressBar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.progressBar2.Location = new System.Drawing.Point(8, 147);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(312, 10);
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar2.TabIndex = 7;
            // 
            // startIpScan
            // 
            this.startIpScan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startIpScan.Location = new System.Drawing.Point(8, 116);
            this.startIpScan.Name = "startIpScan";
            this.startIpScan.Size = new System.Drawing.Size(169, 23);
            this.startIpScan.TabIndex = 4;
            this.startIpScan.Text = "Start";
            this.startIpScan.UseVisualStyleBackColor = true;
            this.startIpScan.Click += new System.EventHandler(this.startIpScan_Click);
            // 
            // scannedHosts
            // 
            this.scannedHosts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scannedHosts.FormattingEnabled = true;
            this.scannedHosts.Location = new System.Drawing.Point(183, 7);
            this.scannedHosts.Name = "scannedHosts";
            this.scannedHosts.Size = new System.Drawing.Size(137, 132);
            this.scannedHosts.TabIndex = 3;
            this.scannedHosts.SelectedIndexChanged += new System.EventHandler(this.scannedHosts_SelectedIndexChanged);
            // 
            // tabPrefs
            // 
            this.tabPrefs.Controls.Add(this.propertyGrid3);
            this.tabPrefs.Location = new System.Drawing.Point(4, 22);
            this.tabPrefs.Name = "tabPrefs";
            this.tabPrefs.Padding = new System.Windows.Forms.Padding(3);
            this.tabPrefs.Size = new System.Drawing.Size(328, 167);
            this.tabPrefs.TabIndex = 1;
            this.tabPrefs.Text = "Prefs";
            this.tabPrefs.UseVisualStyleBackColor = true;
            // 
            // propertyGrid3
            // 
            this.propertyGrid3.HelpVisible = false;
            this.propertyGrid3.Location = new System.Drawing.Point(8, 6);
            this.propertyGrid3.Name = "propertyGrid3";
            this.propertyGrid3.Size = new System.Drawing.Size(314, 153);
            this.propertyGrid3.TabIndex = 0;
            this.propertyGrid3.ToolbarVisible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerSupportsCancellation = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 193);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "pscan";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabScanPorts.ResumeLayout(false);
            this.tabScanPorts.PerformLayout();
            this.tabScanIps.ResumeLayout(false);
            this.tabPrefs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabScanPorts;
        private System.Windows.Forms.TabPage tabScanIps;
        private System.Windows.Forms.Button buttonStart;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListBox scannedPorts;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.TextBox status;
        private System.Net.NetworkInformation.Ping ping1;
        private System.Windows.Forms.Button startIpScan;
        private System.Windows.Forms.ListBox scannedHosts;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.PropertyGrid propertyGrid2;
        private System.Windows.Forms.TabPage tabPrefs;
        private System.Windows.Forms.PropertyGrid propertyGrid3;

    }
}

