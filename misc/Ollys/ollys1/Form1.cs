using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ollys
{
    public partial class Form1 : Form
    {
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

        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);

        private string RequestMACAddress(string IP)
        {
            string macAddress = "unbekannt";
            try
            {
                IPAddress addr = IPAddress.Parse(IP);
                byte[] mac = new byte[6];
                int length = mac.Length;
                SendARP((int)addr.Address, 0, mac, ref length);
                macAddress = BitConverter.ToString(mac, 0, length);
            }
            catch (Exception) { }

            return macAddress;
        }

        // SetWindowPos aus user32.dll holen,
        // selbes prinzip wie oben:
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // fenster verschieben:
                int x = Screen.PrimaryScreen.Bounds.Width - this.Width;
                int y = 100; // rand nach oben
                SetWindowPos(this.Handle, 0, x, y, this.Width, this.Height, 0x014);// SWP_NOZORDER | SWP_SHOWWINDOW; 
            }
            catch (Exception e1) { MessageBox.Show("fenster verschieben ging schief!\r\n" + e1.ToString() ); }

            string HostName = "unbekannt";
            //string IpAdresse = "unbekannt";
            //string macAdresse = "unbekannt";

            try
            {
                HostName = System.Net.Dns.GetHostName();
            }
            catch (Exception e2) { MessageBox.Show("gethostname ging schief!\r\n" + e2.ToString()); }
            label2.Text = HostName;

            try
            {
                System.Net.IPHostEntry hostInfo = System.Net.Dns.GetHostByName(HostName);
                var lab = new Label[14] {  this.label5, this.label6,
                    this.label7, this.label8,
                    this.label9, this.label10,
                    this.label11, this.label12,
                    this.label13, this.label14,
                    this.label15, this.label16,
                    this.label17, this.label18
                };
                int lc = 0;
                foreach ( IPAddress addr in hostInfo.AddressList )
                {
                    Label k = lab[lc++];
                    Label v = lab[lc++];
                    k.Text = "Ip";
                    v.Text = addr.ToString();
                    k = lab[lc++];
                    v = lab[lc++];
                    k.Text = "Mac";
                    v.Text = RequestMACAddress( addr.ToString() );
                }
                string oip = outerIP();
                if (oip != null)
                {
                    lab[lc++].Text = "Outer IP";
                    lab[lc++].Text = oip;
                }
            }
            catch (Exception e3) { MessageBox.Show("ipadresse nicht gefunden!\r\n" + e3.ToString()); }
        }

        private string outerIP()
        {
            string res = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://p4p4.p4.ohost.de/php/ipaddr.php");
                request.ContentType = "text/plain";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream st = response.GetResponseStream();
                StreamReader strm = new StreamReader(st);
                string rs = strm.ReadLine();
                int stop = rs.IndexOf("<");
                if (stop > -1)
                {
                    res += rs.Substring(0, stop);
                }
                else
                {
                    res += rs;
                }
                strm.Close();
            }
            catch (Exception e3) { /*MessageBox.Show(e3.ToString());*/ }
            return res;
        }
    }
}
