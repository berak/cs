using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            process = null;
            unpickle();

            toolTip1.SetToolTip(this.textBox1, "your ftp host, no'ftp' or 'http' !");
            toolTip1.SetToolTip(this.textBox2, "user name for your ftp account");
            toolTip1.SetToolTip(this.textBox7, "password for your ftp account");
            toolTip1.SetToolTip(this.textBox8, "directory on your ftp host,\nmake shure, it exists!");
            toolTip1.SetToolTip(this.textBox10, "the name of the picture\n(must be png!!!)");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (process != null)
            {
                try
                {
                    process.Kill();
                    process.Dispose();
                    process = null;
                }
                catch (Exception) { }
                gray(true);
            }
            else
            {
                string args = " -host " + textBox1.Text
                            + " -user " + textBox2.Text
                            + " -pass " + textBox7.Text
                            + " -dir "  + textBox8.Text
                            + " -pic "  + textBox10.Text
                            + " -vsrc " + (checkBox1.Checked ? "camera_opt" : "camera")
                            + " -vdst " + (checkBox2.Checked ? "'Video Renderer'" : "NullRenderer")
                            + " -time " + numericUpDown1.Value.ToString()
                            + (checkBox4.Checked ? " -ext " : "");

                pickle();
                gray(false);
                startProcess( "websnap", args, (! checkBox4.Checked) );
            }
        }
        private void startProcess(string s, string a, bool win)
        {
            try
            {
                process = new Process();
                process.StartInfo.FileName = s;
                process.StartInfo.Arguments = a;
                process.StartInfo.WorkingDirectory = ".";
                process.StartInfo.CreateNoWindow = win;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = false;
                process.StartInfo.RedirectStandardError = false;
                process.Start();
                //process.WaitForExit();
            }
            catch (Exception)
            {
            }
        }
        private void gray(bool on)
        {
            textBox1.Enabled = on;
            textBox2.Enabled = on;
            textBox7.Enabled = on;
            textBox8.Enabled = on;
            textBox10.Enabled = on;
            checkBox1.Enabled = on;
            checkBox2.Enabled = on;
            checkBox4.Enabled = on;
            numericUpDown1.Enabled = on;
        }

        ///
        ///  cmd box serialization:
        ///
        private void pickle()
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter("snap.txt");
                file.WriteLine(textBox1.Text);
                file.WriteLine(textBox2.Text);
                file.WriteLine(textBox7.Text);
                file.WriteLine(textBox8.Text);
                file.WriteLine(textBox10.Text);
                file.WriteLine(checkBox1.Checked.ToString());
                file.WriteLine(checkBox2.Checked.ToString());
                file.WriteLine(checkBox4.Checked.ToString());
                file.WriteLine(numericUpDown1.Value.ToString());
                file.Close();
            }
            catch (Exception) { }
        }
        private void unpickle()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("snap.txt");
                textBox1.Text = file.ReadLine();
                textBox2.Text = file.ReadLine();
                textBox7.Text = file.ReadLine();
                textBox8.Text = file.ReadLine();
                textBox10.Text = file.ReadLine();
                checkBox1.Checked = (file.ReadLine() == "True" ? true : false);
                checkBox2.Checked = (file.ReadLine() == "True" ? true : false);
                checkBox4.Checked = (file.ReadLine() == "True" ? true : false);
                numericUpDown1.Value = int.Parse(file.ReadLine());
                file.Close();
            }
            catch (Exception) { }
        }

    }
}
