using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.serialPort1.PortName = "COM4";
            this.serialPort1.Open();
            //this.backgroundWorker1.WorkerSupportsCancellation = true;
            //this.backgroundWorker1.RunWorkerAsync();
        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    while (this.serialPort1.IsOpen)
        //    {
        //        int nr = this.serialPort1.Read(buffer, 0, 1024);
        //        if ( nr > 0 )
        //            this.textBox2.Text += "\r\n" + buffer.ToString();
        //    }
        //}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (this.serialPort1.IsOpen)
            {
                this.serialPort1.Write(this.textBox1.Text);

                //char[] buffer = new char[1024];
                //int nr = this.serialPort1.Read(buffer, 0, 1024);
                //if (nr > 0)
                //    this.textBox2.Text += "\r\n" + new string(buffer);
                string s = this.serialPort1.ReadExisting();
                if ( s != null && s.Length>0 )
                    this.textBox2.Text += "\r\n" + s;
            }
        }
    }
}
