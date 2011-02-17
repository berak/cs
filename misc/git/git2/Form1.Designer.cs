namespace git2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbout = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buPush = new System.Windows.Forms.ToolStripButton();
            this.buCommit = new System.Windows.Forms.ToolStripButton();
            this.tbinp = new System.Windows.Forms.ToolStripTextBox();
            this.buSetup = new System.Windows.Forms.ToolStripButton();
            this.buAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbout
            // 
            this.tbout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbout.BackColor = System.Drawing.SystemColors.ControlDark;
            this.tbout.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.tbout.Location = new System.Drawing.Point(0, 28);
            this.tbout.Multiline = true;
            this.tbout.Name = "tbout";
            this.tbout.ReadOnly = true;
            this.tbout.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbout.Size = new System.Drawing.Size(389, 189);
            this.tbout.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buPush,
            this.buCommit,
            this.tbinp,
            this.buSetup,
            this.buAdd});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(389, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buPush
            // 
            this.buPush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buPush.Image = ((System.Drawing.Image)(resources.GetObject("buPush.Image")));
            this.buPush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buPush.Name = "buPush";
            this.buPush.Size = new System.Drawing.Size(34, 22);
            this.buPush.Text = "Push";
            this.buPush.Click += new System.EventHandler(this.buPush_Click);
            // 
            // buCommit
            // 
            this.buCommit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buCommit.Image = ((System.Drawing.Image)(resources.GetObject("buCommit.Image")));
            this.buCommit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buCommit.Name = "buCommit";
            this.buCommit.Size = new System.Drawing.Size(46, 22);
            this.buCommit.Text = "Commit";
            this.buCommit.Click += new System.EventHandler(this.buCommit_Click);
            // 
            // tbinp
            // 
            this.tbinp.Name = "tbinp";
            this.tbinp.Size = new System.Drawing.Size(180, 25);
            // 
            // buSetup
            // 
            this.buSetup.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.buSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buSetup.Image = ((System.Drawing.Image)(resources.GetObject("buSetup.Image")));
            this.buSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buSetup.Name = "buSetup";
            this.buSetup.Size = new System.Drawing.Size(39, 22);
            this.buSetup.Text = "Setup";
            this.buSetup.Click += new System.EventHandler(this.buSetup_Click_1);
            // 
            // buAdd
            // 
            this.buAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.buAdd.Image = ((System.Drawing.Image)(resources.GetObject("buAdd.Image")));
            this.buAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buAdd.Name = "buAdd";
            this.buAdd.Size = new System.Drawing.Size(30, 22);
            this.buAdd.Text = "Add";
            this.buAdd.Click += new System.EventHandler(this.buAdd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 222);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tbout);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbout;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buCommit;
        private System.Windows.Forms.ToolStripButton buPush;
        private System.Windows.Forms.ToolStripButton buSetup;
        private System.Windows.Forms.ToolStripTextBox tbinp;
        private System.Windows.Forms.ToolStripButton buAdd;
    }
}

