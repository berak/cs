namespace soapbar
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.servicesCombo = new System.Windows.Forms.ToolStripComboBox();
            this.queryButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.props = new System.Windows.Forms.PropertyGrid();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.servicesCombo,
            this.queryButton,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(324, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // servicesCombo
            // 
            this.servicesCombo.Name = "servicesCombo";
            this.servicesCombo.Size = new System.Drawing.Size(181, 25);
            this.servicesCombo.SelectedIndexChanged += new System.EventHandler(this.servicesCombo_SelectedIndexChanged);
            this.servicesCombo.Click += new System.EventHandler(this.servicesCombo_Click);
            // 
            // queryButton
            // 
            this.queryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.queryButton.Image = ((System.Drawing.Image)(resources.GetObject("queryButton.Image")));
            this.queryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.queryButton.Name = "queryButton";
            this.queryButton.Size = new System.Drawing.Size(39, 22);
            this.queryButton.Text = "query";
            this.queryButton.Click += new System.EventHandler(this.queryButton_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem});
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripButton1.Size = new System.Drawing.Size(47, 22);
            this.toolStripButton1.Text = "props";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.expandAllToolStripMenuItem.Text = "expand all";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.collapseAllToolStripMenuItem.Text = "collapse all";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // props
            // 
            this.props.Dock = System.Windows.Forms.DockStyle.Fill;
            this.props.HelpVisible = false;
            this.props.Location = new System.Drawing.Point(0, 25);
            this.props.Name = "props";
            this.props.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.props.Size = new System.Drawing.Size(324, 240);
            this.props.TabIndex = 1;
            this.props.ToolbarVisible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 265);
            this.Controls.Add(this.props);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Soap Box";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox servicesCombo;
        private System.Windows.Forms.ToolStripButton queryButton;
        private System.Windows.Forms.PropertyGrid props;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
    }
}

