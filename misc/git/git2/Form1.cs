using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace git2
{
    public partial class Form1 : Form
    {
        private string path = Directory.GetCurrentDirectory();
        public Form1()
        {
            InitializeComponent();
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
        }
        void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "git @ " + path;
        }

        private void buCommit_Click(object sender, EventArgs e)
        {
            tbout.Text = "";
            string mess = tbinp.Text;
            if (mess == null || mess == "")
            {
                tbout.Text = "please add a message";
                return;
            }
            string res = Spawn.SpawnCallback.spawn("git.bat", null, "commit -m " + mess, ".");
            tbout.Text = res.Replace("\n", "\r\n");
            tbinp.Text = "";
        }

        private void buPush_Click(object sender, EventArgs e)
        {
            tbout.Text = "";
            string res = Spawn.SpawnCallback.spawn("git.bat", null, "push origin master", ".");
            tbout.Text = res.Replace("\n", "\r\n");
            tbinp.Text = "";
        }

        private void buSetup_Click_1(object sender, EventArgs e)
        {
            tbout.Text = "";
            string res = Spawn.SpawnCallback.spawn("git_setup.bat", null, tbinp.Text, ".");
            tbout.Text = res.Replace("\n", "\r\n");
            tbinp.Text = "";
        }

        private void buAdd_Click(object sender, EventArgs e)
        {
            tbout.Text = "";
            string mess = tbinp.Text;
            if (mess == null || mess == "")
            {
                tbout.Text = "please add a message";
                return;
            }
            string res = Spawn.SpawnCallback.spawn("git.bat", null, " add " + mess, ".");
            tbout.Text = res.Replace("\n", "\r\n");
            tbinp.Text = "";
        }
    }
}
