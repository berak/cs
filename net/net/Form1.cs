using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace net
{
    public partial class Form1 : Form
    {
        static int nc = 0;
        private System.Net.Sockets.TcpClient sock;
        System.IO.StreamWriter logFile;

        public Form1()
        {
            InitializeComponent();

            this.comboBox1.KeyDown += new KeyEventHandler(combo1_TextChanged);
            this.sock = null;

            logFile = null;

            unpickle();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (sock != null)
            {
                sock.Client.Close();
                sock.Close();
                sock = null;
                this.toolStripButton1.Text = "Connect";
            }
            else 
            {
                try
                {
                    int port = int.Parse(this.toolStripTextBox2.Text);
                    // server:
                    if (this.toolStripTextBox1.Text == null || this.toolStripTextBox1.Text == "" || this.toolStripTextBox1.Text == "0" || this.toolStripTextBox1.Text == "0.0.0.0")
                    {
                        this.textBox2.Text += "Waiting for client on " + port + "\r\n";
                        TcpListener listener = new TcpListener(IPAddress.Any, port);
                        listener.Start();
                        sock = listener.AcceptTcpClient();
                        write( "hello client " + nc++ );
                        sock.Close();
                        sock = null;
                        listener.Stop();
                        listener = null;
                        this.textBox2.Text += "Connection on " + port  + " reset.\r\n";
                        return;
                    }
                    else // client:
                    {
                        sock = new TcpClient(this.toolStripTextBox1.Text, port);
                    }

                    if (sock != null)
                    {
                        this.backgroundWorker1.RunWorkerAsync();
                        this.toolStripButton1.Text = "Close";
                    }
                }
                catch (Exception ex) 
                {
                    this.textBox2.Text = ex.Message + "\r\n";
                    if (sock != null)
                    {
                        this.backgroundWorker1.CancelAsync();
                        sock.Client.Close();
                        sock.Close();
                        sock = null;
                    }
                }
            }

            if (!this.toolStripTextBox1.Items.Contains(this.toolStripTextBox1.Text))
                this.toolStripTextBox1.Items.Add(this.toolStripTextBox1.Text);
            if (!this.toolStripTextBox2.Items.Contains(this.toolStripTextBox2.Text))
                this.toolStripTextBox2.Items.Add(this.toolStripTextBox2.Text);
        }

        private void read()
        {
            try
            {
                // Get the stream
                Stream strm = sock.GetStream();
                byte[] buf = new byte[1024];

                // Read the stream and convert it to ASCII
                if( strm.Read(buf, 0, 1024) > 0)
                {
                    string s = Encoding.ASCII.GetString(buf) + "\n";
                    this.textBox2.Text += s;
                    if (logFile != null)
                        logFile.Write(s);
                }
            }
            catch (Exception) { }
        }

        private void write(string txt)
        {
            try
            {
                byte[] buf = Encoding.ASCII.GetBytes(txt.ToCharArray());
                Stream strm = sock.GetStream();
                strm.Write(buf, 0, buf.Length);
            }
            catch (Exception e) { }
        }
        
        private void combo1_TextChanged(object sender, EventArgs e)
        {
            if ( (sock!= null) && (((KeyEventArgs)e).KeyValue == 13))
            {
                if ( this.comboBox1.Text != null)
                {
                    string txt = this.comboBox1.Text + "\r\n";
                    write(txt);
                    this.textBox2.Text += txt;

                    if (!this.comboBox1.Items.Contains(this.comboBox1.Text))
                        this.comboBox1.Items.Add(this.comboBox1.Text);
                }

                //read();
            }
        }

        
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = "";
        }


        private void pickle()
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter("net.txt");
                for (int i = 0; i < toolStripTextBox1.Items.Count; i++)
                {
                    file.WriteLine(toolStripTextBox1.Items[i].ToString());
                }
                file.WriteLine("#");
                for (int i = 0; i < toolStripTextBox2.Items.Count; i++)
                {
                    file.WriteLine(toolStripTextBox2.Items[i].ToString());
                }
                file.WriteLine("#");
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    file.WriteLine(comboBox1.Items[i].ToString());
                }
                file.Close();
            }
            catch (Exception) { }
        }
        
        
        private void unpickle()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("net.txt");
                while (true)
                {
                    string s = file.ReadLine();
                    if (s == "#") break;
                    toolStripTextBox1.Items.Add(s);
                }
                while (true)
                {
                    string s = file.ReadLine();
                    if (s == "#") break;
                    toolStripTextBox2.Items.Add(s);
                }
                while (true)
                {
                    string s = file.ReadLine();
                    if (s == "#") break;
                    comboBox1.Items.Add(s);
                }
                file.Close();
            }
            catch (Exception) { }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (sock != null)
            {
                read();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (logFile!=null)
            {
                logFile.Close();
                logFile = null;
            }
            else
            {
                logFile = new System.IO.StreamWriter("net_log.txt", true);
            }
        }
    }
}
