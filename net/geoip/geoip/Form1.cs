using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;


namespace geoip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
            this.ipCombo.KeyDown += new KeyEventHandler( ipCombo_KeyDown );
            unpickle();
        }
        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // break on escape, i can't live without it!
            if (e.KeyChar == 27)
                this.Close();
        }


        public void ipCombo_KeyDown(object sender, KeyEventArgs e)
        {
            if (((KeyEventArgs)e).KeyValue == 13)
            {
                if (this.ipCombo.Text != "")
                {
                    findButton.PerformClick(); // press it visibly
                }
            }
        }

        private void osmSearchCompleted(object sender, EventArgs e)
        {
            textBox.Enabled = true;
            MapControl.SearchProvider search = (MapControl.SearchProvider)sender;
            foreach (MapControl.SearchResult r in search.Results)
            {
                textBox.Text += r.DisplayName + "\r\n    " + r.Latitude + " " + r.Longitude + "\r\n";
            }
        }

        private string findIp(string Hostname)
        {
            try
            {
                if (Uri.CheckHostName(Hostname) == UriHostNameType.Unknown)
                {
                    return null;
                }
                IPHostEntry host = Dns.GetHostEntry(Hostname);
                return host.AddressList[0].ToString();
            }
            catch (Exception) { }
            return null;
        }
        
        // this is the free lookup, sometimes does not find stuff.
        private string getlatlon(string ip)
        {
            string ll = null;
            WebClient wc = new WebClient();
            Stream str = wc.OpenRead("http://api.hostip.info/get_html.php?ip="+ip+"&position=true");
            StreamReader sr = new StreamReader(str);
            while ( true )
            { 
                string line = sr.ReadLine();
                if ( line == null )
                    break;
                if ( line.StartsWith("Latitude: " ) )
                    ll = line.Substring( 10 );
                else
                if ( line.StartsWith("Longitude: " ) )
                    ll += " " + line.Substring( 11 );
                else
                    textBox.Text += line + "\r\n";
            }
            textBox.Text += "latlon: " + ll + "\r\n";
            textBox.Text += "(fetched from http://api.hostip.info)\r\n";
            return ll;
        }

        private string whois(string whoisServer, string url)
        {
            StringBuilder stringBuilderResult = new StringBuilder();
            TcpClient tcpClinetWhois = new TcpClient(whoisServer, 43);
            NetworkStream networkStreamWhois = tcpClinetWhois.GetStream();
            BufferedStream bufferedStreamWhois = new BufferedStream(networkStreamWhois);
            StreamWriter streamWriter = new StreamWriter(bufferedStreamWhois);

            streamWriter.WriteLine(url);
            streamWriter.Flush();

            StreamReader streamReaderReceive = new StreamReader(bufferedStreamWhois);

            while (!streamReaderReceive.EndOfStream)
                stringBuilderResult.AppendLine(streamReaderReceive.ReadLine());

            return stringBuilderResult.ToString();
        }

        private void getMap(string lat, string lon)
        {
            try
            {   // find map for latlon:
                string query = "http://tah.openstreetmap.org/MapOf/index.php?lat=" + lat + "&long=" + lon + "&z=" + zoomBox.Text + "&w=400&h=400&format=png";
                query = query.Replace(',', '.'); // arghhh, i HATE localization!!!!!(german, in this case)
                pictureBox1.Load(query);
            }
            catch (Exception ex) { textBox.Text += "\r\n\r\n" + ex.Message; return; }
        }
        
        private void findButton_Click(object sender, EventArgs e)
        {
            if (textBox.SelectedText != "")
            {   // check if a lat-lon was selected:
                string[] ss = textBox.SelectedText.Split(" ".ToCharArray());
                if (ss.Length == 2)
                {
                    if ((Double.Parse(ss[0]) != 0) && (Double.Parse(ss[1]) != 0))
                    {
                        ipCombo.Text = textBox.SelectedText;
                    }
                }
            }
            string lat = "", lon = "";
            textBox.Text = "";
            textBox.Enabled = false;

            // chop copy&paste blanks:
            while (ipCombo.Text.StartsWith(" "))
               ipCombo.Text = ipCombo.Text.Remove(0, 1);

            //check for ip or hostname
            string ip = findIp(ipCombo.Text);
            if (ip != null)
            {
                // check free lookup first
                string ll = getlatlon(ip);
                if (ll != null && ll != " ")
                {
                    string[] ss = ll.Split(" ".ToCharArray());
                    if (ss.Length == 2)
                    {
                        if ((Double.Parse(ss[0]) != 0) && (Double.Parse(ss[1]) != 0))
                        {
                            lat = ss[0];
                            lon = ss[1];
                        }
                        textBox.Enabled = true;
                    }
                }
                if ( lat=="" && lon=="" )
                {
                    // restricted to 40 shots a day.
                    IP2Geo geo = new IP2Geo();
                    IPInformation res = null;
                    try
                    {   // find latlon from ip or hostname:
                        res = geo.ResolveIP(ip, "0"); // 0 is the limited testkey.
                        if (res == null) return;
                        if (res.Longitude == 0 && res.Latitude == 0)
                        {
                            textBox.Text = res.Organization; // please wait or get a key ...
                            return;
                        }
                        textBox.Enabled = true;
                        textBox.Text = res.City + " (" + res.Country + ")\r\n" +
                                       "latlon:" + res.Latitude + " " + res.Longitude + "\r\n" +
                                       "ip:" + ip;
                        lat = "" + res.Latitude;
                        lon = "" + res.Longitude;
                        textBox.Text += "\r\n(fetched from http://ws.cdyne.com)\r\n";
                    }
                    catch (Exception ex) { textBox.Text += "\r\n\r\n" + ex.Message; return; }
                }
                //textBox.Text += "\r\n--- whois " + ip + "\r\n";
                //textBox.Text += whois("whois.internic.com", ip); ;
            }

            // check for latlon in input:
            if (lat == "" && lon == "")
            {
                string[] ss = ipCombo.Text.Split(" ".ToCharArray());
                if (ss.Length == 2)
                {
                    if ((Double.Parse(ss[0]) != 0) && (Double.Parse(ss[1]) != 0))
                    {
                        lat = ss[0];
                        lon = ss[1];
                    }
                }
            }
            // still not found?! check osm for place names :
            if (lat == "" && lon == "")
            {
                MapControl.SearchProvider search = new MapControl.SearchProvider();
                search.SearchCompleted += new EventHandler(osmSearchCompleted);
                if (search.Search(ipCombo.Text))
                {
                    textBox.Enabled = false;
                }
            }

            if (lat != "" && lon != "")
            {
                getMap(lat, lon);
            }
            if (!this.ipCombo.Items.Contains(this.ipCombo.Text))
                this.ipCombo.Items.Add(this.ipCombo.Text);
        }

        private void label_click(int id, double off)
        {
            string[] ss = ipCombo.Text.Split(" ".ToCharArray());
            if (ss.Length == 2)
            {
                double [] latlon = new double[2] {Double.Parse(ss[0]),Double.Parse(ss[1])};
                if ((latlon[0] != 0) && (latlon[1] != 0))
                {
                    double f = off / (18-Double.Parse(zoomBox.Text));
                    ipCombo.Text = ipCombo.Text.Replace(ss[id], ("" + (latlon[id] + f)));
                    getMap(ss[0], ss[1]);
                }
            }
        }
        private void labelL_Click(object sender, EventArgs e)
        {
            label_click(1, -0.02);
        }
        private void labelU_Click(object sender, EventArgs e)
        {
            label_click(0,  0.02);
        }
        private void labelD_Click(object sender, EventArgs e)
        {
            label_click(0, -0.02);
        }
        private void labelR_Click(object sender, EventArgs e)
        {
            label_click(1,  0.02);
        }

        private void pickle()
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter("ip.txt");
                for (int i = 0; i < ipCombo.Items.Count; i++)
                {
                    file.WriteLine(ipCombo.Items[i].ToString());
                }
                file.WriteLine("#");
                file.Close();
            }
            catch (Exception) { }
        }
        private void unpickle()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader("ip.txt");
                while (true)
                {
                    string s = file.ReadLine();
                    if (s == "#") break;
                    ipCombo.Items.Add(s);
                }
                file.Close();
            }
            catch (Exception) { }
        }

    }
}
