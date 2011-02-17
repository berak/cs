using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;


namespace pscan
{
    public partial class Form1 : Form
    {
        Hashtable ports; // names of known ports
        System.IO.StreamWriter logFile = null;

        struct ScanPortData
        {
            public string ip;
            public int from;
            public int to;
            // [CategoryAttribute("Scan"), DescriptionAttribute("Target Ip")]
            public string Ip
            {
                get { return ip; }
                set { ip = value; }
            }
            public int From
            {
                get { return from; }
                set { from = value; }
            }
            public int To
            {
                get { return to; }
                set { to = value; }
            }
        }
        ScanPortData scanPorts;

        struct ScanIpData
        {
            public string start;
            public string stop;
            public int port;

            public string Start
            {
                get { return start; }
                set { start = value; }
            }
            public string Stop
            {
                get { return stop; }
                set { stop = value; }
            }
            public int Port
            {
                get { return port; }
                set { port = value; }
            }
        }
        ScanIpData scanIps;

        struct PrefsData
        {
            public int timeout;
            public string browser;
            public bool autoBrowse;
            public bool check401;
            public bool doLog;

            public int Timeout
            {
                get { return timeout; }
                set { timeout = value; }
            }
            public string Browser
            {
                get { return browser; }
                set { browser = value; }
            }
            public bool AutoBrowse
            {
                get { return autoBrowse; }
                set { autoBrowse = value; }
            }
            public bool Check401
            {
                get { return check401; }
                set { check401 = value; }
            }
            public bool DoLog
            {
                get { return doLog; }
                set { doLog = value; }
            }

        }
        PrefsData prefs;

        public Form1()
        {
            InitializeComponent();
            ports = new Hashtable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            scanPorts = new ScanPortData();
            scanPorts.Ip = "85.183.54.25";
            scanPorts.From = 1;
            scanPorts.To = 1024;
            propertyGrid1.SelectedObject = scanPorts;

            scanIps = new ScanIpData();
            scanIps.Start = "85.183.54.0";
            scanIps.Stop = "85.183.54.50";
            scanIps.Port = 80;
            propertyGrid2.SelectedObject = scanIps;

            prefs = new PrefsData();
            prefs.Timeout = 5000;
            prefs.Check401 = true;
            prefs.AutoBrowse = true;
            prefs.Browser = "firefox";
            propertyGrid3.SelectedObject = prefs;
            prefs.DoLog = true;

            unpickle();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text == "Start")
            {
                string host = scanPorts.ip;
                try
                {
                    scannedPorts.Items.Clear();
                    backgroundWorker1.RunWorkerAsync();
                    buttonStart.Text = "Stop";
                    pickle();
                }
                catch (Exception) { }
                return;
            }
            if (buttonStart.Text == "Stop")
            {
                //backgroundWorker1.CancelAsync();
                buttonStart.Text = "Stopping";
                return;
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            scanPorts = (ScanPortData)propertyGrid1.SelectedObject;
            prefs  = (PrefsData)propertyGrid3.SelectedObject;
            int timeout = prefs.Timeout;

            MethodInvoker ScanStart = delegate
            {
                scannedPorts.Items.Clear();
                progressBar1.Maximum = scanPorts.To - scanPorts.From;
                progressBar1.Value = 0;
            };
            Invoke(ScanStart);

            PortScanner[] scanner = new PortScanner[16];
            ManualResetEvent[] reset = new ManualResetEvent[16];
            for (int i = 0; i < 16; i++)
            {
                scanner[i] = new PortScanner(scanPorts.Ip, 0);
                scanner[i].timeout = prefs.timeout;
            }

            while (scanPorts.From < scanPorts.To && ((buttonStart.Text == "Stop")))
            {
                for (int i = 0; i < 16; i++)
                {
                    scanner[i].PORT = scanPorts.From++;
                    reset[i] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(scanner[i].Run, reset[i]);
                }
                WaitHandle.WaitAll(reset, timeout);
                for (int i = 0; i < 16; i++)
                {
                    bool hit = (scanner[i].result > 0);
                    MethodInvoker ScanUpdate = delegate
                    {
                        int p = scanner[i].PORT;
                        progressBar1.Increment(1);
                        status.Text = p + "    " + ports[p];
                        scanPorts.From = p;
                        propertyGrid1.SelectedObject = scanPorts;
                        string name = p + "\t" + ports[p];
                        if ((hit) && (!scannedPorts.Items.Contains(name)))
                            scannedPorts.Items.Add(name);
                    };
                    Invoke(ScanUpdate);
                }
            }
            buttonStart.Text = "Start";
            //int ntw = 0;
            //int ncp = 0;
            //ThreadPool.GetAvailableThreads(out ntw, out ncp);
            //Thread.Sleep(600);
        }


        private void startIpScan_Click(object sender, EventArgs e)
        {
            if (startIpScan.Text == "Start")
            {
                backgroundWorker2.RunWorkerAsync();
                startIpScan.Text = "Stop";
            }
            else
            {
               // backgroundWorker2.CancelAsync();
                startIpScan.Text = "Stopping";
            }
        }

        private string incIp(string ip)
        {
            string[] ss = ip.Split(".".ToCharArray());
            int e0 = int.Parse(ss[3]);
            int e1 = int.Parse(ss[2]);
            int e2 = int.Parse(ss[1]);
            int e3 = int.Parse(ss[0]);
            e0++;
            if (e0 > 255) { e0 = 0; e1++; }
            if (e1 > 255) { e1 = 0; e2++; }
            if (e2 > 255) { e2 = 0; e3++; }
            return e3 + "." + e2 + "." + e1 + "." + e0;
        }
        private int toInt(string ip)
        {
            string[] ss = ip.Split(".".ToCharArray());
            int e0 = int.Parse(ss[3]);
            int e1 = int.Parse(ss[2])*0xff;
            int e2 = int.Parse(ss[1])*0xff00;
            int e3 = int.Parse(ss[0])*0xff0000;
            return e3 + e2 + e1 + e0;
        }

        private void browse(string ip, int port)
        {
            if (port == 80 || port == 81 || port == 8080)
            {
                Browser b = new Browser(ip, port, prefs.Check401, prefs.Browser);
                Thread brt = new Thread(new ThreadStart(b.run));
                brt.Start();
            }
        }
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            scanIps = (ScanIpData)propertyGrid2.SelectedObject;
            string start = scanIps.Start;
            string stop = scanIps.Stop;
            int port = scanIps.Port;
            prefs = (PrefsData)propertyGrid3.SelectedObject;
            int timeout = prefs.Timeout;
            if (prefs.DoLog)
            {
                try
                {
                    logFile = new System.IO.StreamWriter("pscan_"+start+"_"+stop+".html");
                }
                catch (Exception) { }
            }

            PortScanner[] scanner = new PortScanner[16];
            ManualResetEvent[] reset = new ManualResetEvent[16];
            for (int i = 0; i < 16; i++)
            {
                scanner[i] = new PortScanner("", 0);
                scanner[i].timeout = timeout;
            }

            MethodInvoker ScanStart = delegate
            {
                scannedHosts.Items.Clear();
                progressBar2.Maximum = toInt(stop) - toInt(start);
                progressBar2.Value = 0;
            };
            Invoke(ScanStart);

            while (startIpScan.Text == "Stop")
            {
                for (int i = 0; i < 16; i++)
                {
                    start = incIp(start);
                    scanner[i].SERVER = start;
                    scanner[i].PORT = port;
                    scanner[i].ping1 = ping1;
                    reset[i] = new ManualResetEvent(false);
                    ThreadPool.QueueUserWorkItem(scanner[i].Run, reset[i]);
                }
                WaitHandle.WaitAll(reset, 5000);
                MethodInvoker ScanUpdate = delegate
                {
                    for (int i = 0; i < 16; i++)
                    {
                        if (scanner[i].result > 0)
                        {
                            scannedHosts.Items.Add(scanner[i].SERVER);
                            if (prefs.autoBrowse)
                            {
                                browse(scanner[i].SERVER, port);
                            }
                            if (logFile != null)
                            {
                                logFile.WriteLine("<a href=\"http://"+scanner[i].SERVER+"\">"+scanner[i].SERVER+"</a><br>\n");
                                logFile.Flush();
                            }
                        }
                        progressBar2.Increment(1);
                    };
                    scanIps.Start = start;
                    propertyGrid2.SelectedObject = scanIps;
                };
                Invoke(ScanUpdate);
                if (toInt(start) >= toInt(stop))
                    break;
            }
            if (logFile != null)
            {
                logFile.Close();
            }
            startIpScan.Text = "Start";
        }



        private void pickle()
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter("pscan.txt");
                //for (int i = 0; i < targetIp.Items.Count; i++)
                //{
                //    file.WriteLine(targetIp.Items[i]);
                //}
                file.WriteLine("#");
                file.Close();
            }
            catch (Exception) { }
        }

        private void unpickle()
        {
            //try
            //{
            //    System.IO.StreamReader file = new System.IO.StreamReader("pscan.txt");
            //    while (file != null)
            //    {
            //        string s0 = file.ReadLine();
            //        if (s0 == null) break;
            //        if (s0 == "#") break;
            //        targetIp.Items.Add(s0);
            //    }
            //    file.Close();
            //    targetIp.SelectedIndex = 0;
            //}
            //catch (Exception) { }
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("ipports.txt");
                while (file != null)
                {
                    string s0 = file.ReadLine();
                    if (s0 == null) break;
                    if (s0.StartsWith("#")) continue; ;
                    try
                    {
                        string[] ss = s0.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (ss[1].Contains("/tcp"))
                        {
                            string[] sx = ss[1].Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            int p = int.Parse(sx[0]);
                            if (ports.Contains(p))
                                continue;
                            ports.Add(p, ss[0]);
                        }
                    }
                    catch (Exception) { }
                }
                file.Close();
            }
            catch (Exception) { }
        }

        private void scannedHosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int port = scanIps.Port;
            string url = scannedHosts.SelectedItem.ToString();
            browse(url, port);
        }
    }

    class PortScanner
    {
        public string SERVER = "0.0.0.0";
        public int PORT = 0;
        public int result = 0;
        public int timeout = 3000;
        public System.Net.NetworkInformation.Ping ping1 = null;

        public PortScanner(string SERVER, int PORT)
        {
            this.SERVER = SERVER;
            this.PORT = PORT;
        }

        public void Run(Object arg)
        {
            result = 0;
            if (PORT != 0)
            {
                try
                {
                    TcpClient irc = new TcpClient(SERVER, PORT);
                    result = PORT;
                }
                catch (Exception) { }
            }
            else if ( ping1 != null )
            {
                byte[] buffer = new byte[16];
                System.Net.NetworkInformation.PingReply reply = ping1.Send(SERVER, timeout, buffer, null);
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    result = 1;
                }
            }

            if (arg != null)
            {
                ManualResetEvent reset = (ManualResetEvent)arg;
                reset.Set();
            }
        }
    }


    public class Browser
    {
        public string ip;
        public int port;
        public bool check401;
        public string browser;

        public Browser(string i, int p, bool c, string b)
        {
            ip = i; port = p; check401 = c; browser = b;
        }

        public void run()
        {
            if (check401)
            {
                try
                {
                    TcpClient c = new TcpClient(ip, port);
                    NetworkStream strm = c.GetStream();
                    string req = "HEAD / HTTP/1.0\r\n"
                               + "ACCEPT: *\r\n"
                               + "\r\n";
                    byte[] buf = Encoding.ASCII.GetBytes(req.ToCharArray());
                    strm.Write(buf, 0, buf.Length);
                    byte[] buf2 = new byte[4024];

                    // Read the stream and convert it to ASCII
                    strm.Read(buf2, 0, 4024);
                    string s = Encoding.ASCII.GetString(buf2);
                    if (s.Contains("400"))   return;
                    if (s.Contains("401"))   return;
                    if (s.Contains("403"))   return;
                    if (s.Contains("404"))   return;
                    if (s.Contains("406"))   return;
                    if (s.IndexOf("www-authenticate", StringComparison.InvariantCultureIgnoreCase) > 0)
                        return;
                    int n = s.IndexOf("Content-Length: ", StringComparison.InvariantCultureIgnoreCase);
                    string s2 = s.Substring(n+16,10);
                    string s3 = s2.Split("\r\n".ToCharArray())[0];
                    int nb = int.Parse(s3);
                    if (nb < 64)
                        return;
                }
                catch (Exception) { return; }
            }
            try 
            {
                Process p = new Process();
                p.StartInfo.FileName = browser;
                p.StartInfo.Arguments = ip + ":" + port;
                p.StartInfo.UseShellExecute = true;
                p.Start();
            }
            catch (Exception) { return; }
        }
    }
}