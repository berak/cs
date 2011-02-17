namespace pmail
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.selectUser = new System.Windows.Forms.ToolStripComboBox();
            this.buttonGet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonSend = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.buttonDel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.getFirstMailOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteOnServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.logTcpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appedLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.transparentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.selectMail = new System.Windows.Forms.ComboBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.mailTo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mailSubj = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mailAttach = new System.Windows.Forms.TextBox();
            this.buttonAttach = new System.Windows.Forms.Button();
            this.mailText = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pingMailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectUser,
            this.buttonGet,
            this.toolStripSeparator2,
            this.buttonSend,
            this.toolStripSeparator1,
            this.buttonDel,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(598, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // selectUser
            // 
            this.selectUser.Name = "selectUser";
            this.selectUser.Size = new System.Drawing.Size(161, 25);
            this.selectUser.ToolTipText = "User Name";
            this.selectUser.SelectedIndexChanged += new System.EventHandler(this.selectUser_SelectedIndexChanged);
            // 
            // buttonGet
            // 
            this.buttonGet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonGet.Image = ((System.Drawing.Image)(resources.GetObject("buttonGet.Image")));
            this.buttonGet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonGet.Name = "buttonGet";
            this.buttonGet.Size = new System.Drawing.Size(49, 22);
            this.buttonGet.Text = "Get Mail";
            this.buttonGet.Click += new System.EventHandler(this.buttonGet_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonSend
            // 
            this.buttonSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonSend.Image = ((System.Drawing.Image)(resources.GetObject("buttonSend.Image")));
            this.buttonSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(56, 22);
            this.buttonSend.Text = "Send Mail";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // buttonDel
            // 
            this.buttonDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buttonDel.Image = ((System.Drawing.Image)(resources.GetObject("buttonDel.Image")));
            this.buttonDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonDel.Name = "buttonDel";
            this.buttonDel.Size = new System.Drawing.Size(42, 22);
            this.buttonDel.Text = "Delete";
            this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.getFirstMailOnlyToolStripMenuItem,
            this.deleteOnServerToolStripMenuItem,
            this.preferTextToolStripMenuItem,
            this.toolStripSeparator5,
            this.logTcpToolStripMenuItem,
            this.appedLogToolStripMenuItem,
            this.showLogToolStripMenuItem,
            this.toolStripSeparator4,
            this.transparentToolStripMenuItem,
            this.toolStripSeparator7,
            this.pingMailToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(45, 22);
            this.toolStripDropDownButton1.Text = "Prefs";
            // 
            // getFirstMailOnlyToolStripMenuItem
            // 
            this.getFirstMailOnlyToolStripMenuItem.CheckOnClick = true;
            this.getFirstMailOnlyToolStripMenuItem.Name = "getFirstMailOnlyToolStripMenuItem";
            this.getFirstMailOnlyToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.getFirstMailOnlyToolStripMenuItem.Text = "Get First Mail Only";
            this.getFirstMailOnlyToolStripMenuItem.Click += new System.EventHandler(this.getFirstMailOnlyToolStripMenuItem_Click);
            // 
            // deleteOnServerToolStripMenuItem
            // 
            this.deleteOnServerToolStripMenuItem.Checked = true;
            this.deleteOnServerToolStripMenuItem.CheckOnClick = true;
            this.deleteOnServerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteOnServerToolStripMenuItem.Name = "deleteOnServerToolStripMenuItem";
            this.deleteOnServerToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.deleteOnServerToolStripMenuItem.Text = "Delete on Server";
            this.deleteOnServerToolStripMenuItem.Click += new System.EventHandler(this.deleteOnServerToolStripMenuItem_Click);
            // 
            // preferTextToolStripMenuItem
            // 
            this.preferTextToolStripMenuItem.Checked = true;
            this.preferTextToolStripMenuItem.CheckOnClick = true;
            this.preferTextToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.preferTextToolStripMenuItem.Name = "preferTextToolStripMenuItem";
            this.preferTextToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.preferTextToolStripMenuItem.Text = "Prefer Text";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(169, 6);
            // 
            // logTcpToolStripMenuItem
            // 
            this.logTcpToolStripMenuItem.Checked = true;
            this.logTcpToolStripMenuItem.CheckOnClick = true;
            this.logTcpToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.logTcpToolStripMenuItem.Name = "logTcpToolStripMenuItem";
            this.logTcpToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.logTcpToolStripMenuItem.Text = "Log Tcp";
            this.logTcpToolStripMenuItem.Click += new System.EventHandler(this.logTcpToolStripMenuItem_Click);
            // 
            // appedLogToolStripMenuItem
            // 
            this.appedLogToolStripMenuItem.Checked = true;
            this.appedLogToolStripMenuItem.CheckOnClick = true;
            this.appedLogToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.appedLogToolStripMenuItem.Name = "appedLogToolStripMenuItem";
            this.appedLogToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.appedLogToolStripMenuItem.Text = "Apped Log";
            this.appedLogToolStripMenuItem.Click += new System.EventHandler(this.appedLogToolStripMenuItem_Click);
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Enabled = false;
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.showLogToolStripMenuItem.Text = "Show Log";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.showLogToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
            // 
            // transparentToolStripMenuItem
            // 
            this.transparentToolStripMenuItem.CheckOnClick = true;
            this.transparentToolStripMenuItem.Name = "transparentToolStripMenuItem";
            this.transparentToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.transparentToolStripMenuItem.Text = "Transparent";
            this.transparentToolStripMenuItem.Click += new System.EventHandler(this.transparentToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // selectMail
            // 
            this.selectMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.selectMail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectMail.FormattingEnabled = true;
            this.selectMail.ItemHeight = 13;
            this.selectMail.Location = new System.Drawing.Point(0, 28);
            this.selectMail.Name = "selectMail";
            this.selectMail.Size = new System.Drawing.Size(598, 21);
            this.selectMail.TabIndex = 2;
            this.selectMail.SelectedIndexChanged += new System.EventHandler(this.selectMail_SelectedIndexChanged);
            // 
            // webBrowser1
            // 
            this.webBrowser1.AccessibleRole = System.Windows.Forms.AccessibleRole.Client;
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(3, 53);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(593, 354);
            this.webBrowser1.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 410);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(598, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.Client;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Visible = false;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(4, 17);
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // mailTo
            // 
            this.mailTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.mailTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.mailTo.Location = new System.Drawing.Point(6, 19);
            this.mailTo.Name = "mailTo";
            this.mailTo.Size = new System.Drawing.Size(318, 20);
            this.mailTo.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mailTo);
            this.groupBox1.Location = new System.Drawing.Point(7, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(333, 51);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "To";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mailSubj);
            this.groupBox2.Location = new System.Drawing.Point(346, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 51);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Subject";
            // 
            // mailSubj
            // 
            this.mailSubj.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.mailSubj.Location = new System.Drawing.Point(6, 20);
            this.mailSubj.Name = "mailSubj";
            this.mailSubj.Size = new System.Drawing.Size(232, 20);
            this.mailSubj.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.mailAttach);
            this.panel1.Controls.Add(this.buttonAttach);
            this.panel1.Controls.Add(this.mailText);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 379);
            this.panel1.TabIndex = 8;
            this.panel1.Visible = false;
            // 
            // mailAttach
            // 
            this.mailAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mailAttach.Location = new System.Drawing.Point(86, 352);
            this.mailAttach.Name = "mailAttach";
            this.mailAttach.Size = new System.Drawing.Size(254, 20);
            this.mailAttach.TabIndex = 10;
            // 
            // buttonAttach
            // 
            this.buttonAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAttach.Location = new System.Drawing.Point(13, 352);
            this.buttonAttach.Name = "buttonAttach";
            this.buttonAttach.Size = new System.Drawing.Size(62, 20);
            this.buttonAttach.TabIndex = 9;
            this.buttonAttach.Text = "Attach";
            this.buttonAttach.UseVisualStyleBackColor = true;
            this.buttonAttach.Click += new System.EventHandler(this.buttonAttach_Click);
            // 
            // mailText
            // 
            this.mailText.AcceptsReturn = true;
            this.mailText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mailText.Location = new System.Drawing.Point(7, 60);
            this.mailText.Multiline = true;
            this.mailText.Name = "mailText";
            this.mailText.Size = new System.Drawing.Size(583, 286);
            this.mailText.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pingMailToolStripMenuItem
            // 
            this.pingMailToolStripMenuItem.Name = "pingMailToolStripMenuItem";
            this.pingMailToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.pingMailToolStripMenuItem.Text = "Ping Mail";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(169, 6);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 432);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.selectMail);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Mail@";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox selectUser;
        private System.Windows.Forms.ToolStripButton buttonGet;
        private System.Windows.Forms.ToolStripButton buttonSend;
        private System.Windows.Forms.ComboBox selectMail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton buttonDel;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem deleteOnServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TextBox mailTo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox mailSubj;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox mailText;
        private System.Windows.Forms.Button buttonAttach;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox mailAttach;
        private System.Windows.Forms.ToolStripMenuItem transparentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logTcpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getFirstMailOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem preferTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appedLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem pingMailToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
    }
}

