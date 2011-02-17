using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;


namespace pmail
{
    public partial class Form1 : Form
    {
        private Hashtable users;
        private Hashtable mime;
        private System.Net.Sockets.TcpClient sock;
        private System.IO.StreamWriter logFile;
        private string curUser, curPass;
        private bool delOnServer;
        private bool firstMail;
        private bool appendLog;

        private string startDir;

        private StreamReader reader;
        private StreamWriter writer;

        private Icon ico_on;
        private Icon ico_off;
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;



        public Form1(string[] args)
        {
            //MessageBox.Show(args..ToString());
            startDir = Directory.GetCurrentDirectory();
            InitializeComponent();
            this.Resize += new EventHandler(onResize);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            users = new Hashtable();
            unpickle();
            selectUser.SelectedIndex = 0;
            ico_on = new Icon(startDir + "\\on.ico");
            ico_off = new Icon(startDir + "\\off.ico");
            trayIcon = new NotifyIcon();
            trayIcon.Icon = ico_off;
            trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Exit", onExit);
            trayMenu.MenuItems.Add("Show", onShow);
            trayIcon.ContextMenu = trayMenu;
 
            mime = new Hashtable();
            appendLog = true;
            delOnServer = false;
            panel1.Visible = false;
            mime["txt"] = "text/plain";
            mime["jpg"] = "image/jpeg";
            mime["jpeg"] = "image/jpeg";
            mime["png"] = "image/png";
            mime["bmp"] = "image/bmp";
            mime["gif"] = "image/gif";
            mime["zip"] = "application/zip";
            mime[""] = "application/unknown";
            try
            {
                logFile = new System.IO.StreamWriter(startDir + "\\log.txt", appendLog);
                writeLogDate("start");
            }
            catch (Exception) { }


            this.timer1.Interval = 1000 * 60 * 17;
            //  timer1_Tick(null, null);
        }

        private void onExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void onShow(object sender, EventArgs e)
        {
            Visible = true;
            ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.Show();
        }
        private void onResize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                trayIcon.Visible = true;
                this.Hide();
                this.timer1.Start();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                trayIcon.Visible = false;
                this.Show();
                this.timer1.Stop();
            }
        }

        private void error(string mess)
        {
            writeLogDate( mess);
            this.toolStripStatusLabel1.Text = mess;
            if (sock != null)
            {
                sock = null;
                reader = null;
                writer = null;
            }
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
        }

        private string attachData(string filename)
        {
            FileInfo f = new FileInfo(filename);
            int n = 0;
            long nbytes = f.Length;
            byte[] rec = new byte[nbytes];
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                BinaryReader r = new BinaryReader(fs);
                n = r.Read(rec, 0, (int)nbytes);
                fs.Close();
            }
            writeLog("[attachment : " + filename + " " + n + "/" + nbytes + "]");
            return System.Convert.ToBase64String(rec,0,n);
        }

        private bool checkResponse(StreamReader reader, string tok)
        {
            string response = readLine();
            if (response==null)
            {
                error("!! NULL checkResponse$");
                return false;
            }
            if (!response.StartsWith(tok))
            {
                int n = response.Length;
                if (n > 40) n = 40;
                error(response.Substring(0,n));
                return false;
            }
            return true;
        }

        
        private string nice( string input )
        {
            input = input.Replace('<',' ');
            input = input.Replace('>',' ');
            input = input.Replace('?', ' ');
            input = input.Replace(':', ' ');
            input = input.Replace('\"', ' ');
            input = input.Replace('\'', ' ');
            input = input.Replace('\\', ' ');
            input = input.Replace('/', ' ');
            return input;
        }


        private void sendMail(string from, string to, string subject, string attach, string mess)
        {
            writeLogDate("send");
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Increment(5);
            string mb = "px12345";
            string[] ss = from.Split('@');
            string user = ss[0];
            string addr = "smtp." + ss[1];
            try
            {
                sock   = new TcpClient(addr, 587); // auth port !!!
                reader = new StreamReader(sock.GetStream());
                writer = new StreamWriter(sock.GetStream());
            }
            catch (Exception) 
            { 
                error("could not connect to " + addr); 
                return; 
            }
            toolStripProgressBar1.Increment(5);


            string response = readLine();
            writeLine("HELO " + user,true);
            toolStripProgressBar1.Increment(5);

            if (!checkResponse(reader, "2"))
                return;
            writeLine("MAIL FROM: <" + from + ">",true);

            if (!checkResponse(reader, "2"))
                return;

            writeLine("RCPT TO: <" + to + ">",true);

            if (!checkResponse(reader, "2"))
                return;

            writeLine("DATA", true);

            if (!checkResponse(reader, "3"))
                return;
            toolStripProgressBar1.Increment(5);

            writeLine("From: " + from, false);
            writeLine("To: " + to, false);
            writeLine("Subject: " + subject, false);
            writeLine("Date: " + DateTime.Now.ToString(), false);
            writeLine("Return-Path: " + from, false);
            writeLine("MIME-Version: 1.0", false);
            toolStripProgressBar1.Increment(5);

            if (attach != "")
            {
                int lx = attach.LastIndexOf('.');
                string ext="";
                if (lx > -1)
                    ext = attach.Substring(lx + 1, attach.Length - lx - 1);
                string a_mime = (string)mime[ext];//"application/unknown";
                if (a_mime == null) a_mime = "text/plain";

                string attachName = attach;
                int ls = attach.LastIndexOf('/');
                int lb = attach.LastIndexOf('\\');
                int l = (ls>lb?ls:lb);
                if ( l>-1 )
                    attachName = attach.Substring(l+1,attach.Length-l-1);

                string att_data = attachData(attach);
                int att_l = att_data.Length;

//                string[] ss = attach.LastIndexOf('/');
                writeLine("Content-type: Multipart/Mixed; boundary=\"" + mb + "\"", false);
                writeLine("", false);
                writeLine("This is a multi-part message in MIME format.", false);
                writeLine("", false);
                writeLine("--" + mb, false);
                writeLine("Content-type: text/plain", false);
                writeLine("Content-transfer-encoding: Quoted-printable", false);
                writeLine("", false);
                writeLine(mess, false);
                toolStripProgressBar1.Increment(5);
                writeLine("", false);
                writeLine("--" + mb, false);
                writeLine("Content-type: " + a_mime + "; name=\"" + attachName+ "\"", false);
                writeLine("Content-length: " + att_l, false);
                writeLine("Content-disposition: attachment; filename=\"" + attachName + "\"", false);
                writeLine("Content-transfer-encoding: BASE64", false);
                writeLine("", false);
                writeLine(att_data, false);
                writeLine("--" + mb, false);
                toolStripProgressBar1.Increment(5);
            }
            else
            {
                writeLine("Content-type: text/plain", false);
                writeLine("", false);
                writeLine(mess, false);
                toolStripProgressBar1.Increment(5);
            }

            writeLine(".", true);
            toolStripProgressBar1.Increment(5);
            if (!checkResponse(reader, "2"))
                return;

            toolStripProgressBar1.Increment(5);
            writeLine("quit", true);

            error("sent mail to " + to);
        }

        private void showMail(string mname, bool unpack)
        {
            bool preferText = preferTextToolStripMenuItem.Checked;
            bool isHtml = false;
            string attachName = null;
            string multipart = null;
            bool dec64 = false;
            string head = "", body = "";

            toolStripStatusLabel2.Text = "";
            try
            {
                System.IO.StreamReader file = null;
                file = new System.IO.StreamReader(startDir + "\\inbox\\" + mname);
                string line = null;
                while (file != null)
                {
                    line = file.ReadLine();
                    if (line == null)
                        break;
                    if (line == "")
                        break;
                    if (line.EndsWith("base64", true, null))
                        dec64 = true;

                    string mu = find(line, "Content-type: Multipart/Mixed; boundary=", null);
                    if (mu == null)
                        mu = find(line, "Content-type: Multipart/alternative; boundary=", null);
                    if (mu != null)
                        mu = mu.Substring(1, mu.Length - 2); // unescape
                    if (mu != null)
                        multipart = mu;

                    string ih = find(line, "Content-type: text/html", null);
                    if (ih != null) isHtml = true;

                    head += line + "\r\n";
                }
                while (file != null)
                {
                    line = file.ReadLine();
                    if (line == null)
                        break;
                    if (line == ".")
                        break;
                    body += line + "\r\n";
                }
                file.Close();
            }
            catch (Exception) { }
            if (dec64 && (multipart == null)) // decode whole body as text
            {
                try
                {
                    byte[] encodedDataAsBytes = System.Convert.FromBase64String(body);
                    body = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
                }
                catch (Exception) { error("err decoding base64!"); }
            }
            else
            {
                if ((multipart != null))
                {
                    multipart = "--" + multipart;
                    string[] ss = Regex.Split(body, multipart);
                    for (int i = 0; i < ss.Length; i++)
                    {
                        string an = find(ss[i], "Content-disposition: attachment; filename=", "\r\n");
                        if (an != null)
                        {
                            if (an.StartsWith("\""))
                                an = an.Substring(1, an.Length - 2); // unescape
                            attachName = an;
                            if (unpack)
                            {
                                int ns = ss[i].IndexOf("\r\n\r\n");
                                string att = ss[i].Substring(ns, ss[i].Length - ns);
                                try
                                {
                                    byte[] encodedData = System.Convert.FromBase64String(att);
                                    using (FileStream fs = new FileStream(startDir + "\\inbox\\" + attachName, FileMode.CreateNew))
                                    {
                                        BinaryWriter w = new BinaryWriter(fs);
                                        w.Write(encodedData, 0, (int)encodedData.Length);
                                        fs.Close();
                                        writeLog("[wrote attachment : " + fs + " " + encodedData.Length + "/" + att.Length + "]");
                                    }
                                }
                                catch (Exception) { }
                            }
                        }

                        if (ss[i].Contains("text/plain"))
                        {
                            int ns = ss[i].IndexOf("\r\n\r\n");
                            body = ss[i].Substring(ns, ss[i].Length - ns);
                            isHtml = false;
                        }
                        if (ss[i].Contains("text/html") && (!preferText))
                        {
                            int ns = ss[i].IndexOf("\r\n\r\n");
                            body = ss[i].Substring(ns, ss[i].Length - ns);
                            isHtml = true;
                            break;
                        }
                    }
                }
            }
            if (0 > body.IndexOf(" quoted-printable", StringComparison.InvariantCultureIgnoreCase))
            {
               // Regex expr = new Regex("=[0-9A-F][0-9A-F]");
                body = body.Replace("=0D", " ");
                body = body.Replace("=0A", " ");
                body = body.Replace("=20", " ");
                body = body.Replace("=3D", "=");
                body = body.Replace("=F6", "ö");
                body = body.Replace("=E4", "ü");
                body = body.Replace("=FC", "ä");
                body = body.Replace("=D6", "ss");
                body = body.Replace("=C4", "Ö");
                body = body.Replace("=DC", "Ü");
                body = body.Replace("=DF", "Ä");
                body = body.Replace("=\r\n", "\r\n");
            }

            //if (0 > body.IndexOf("<html", StringComparison.InvariantCultureIgnoreCase))
            if ( ! isHtml )
            {
                body = "<plaintext>" + body;
            }
            this.webBrowser1.DocumentText = body;
            toolStripStatusLabel2.Text = attachName;
        }

        private int listDir(string uname)
        {
            selectMail.Items.Clear();
            selectMail.Text = "";
            toolStripStatusLabel2.Text = null;
            webBrowser1.DocumentText = "";
            string key = uname.Split('@')[0];
            int hits = 0;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(startDir + "\\inbox");
                foreach (FileSystemInfo fsi in dir.GetFileSystemInfos())
                {
                    try
                    {
                        if (fsi is FileInfo)
                        {
                            FileInfo f = (FileInfo)fsi;
                            if (f.Name.StartsWith(key))
                            {
                                selectMail.Items.Add(f.Name);
                                hits ++;
                            }
                        }
                    }
                    catch (Exception) { } //ignore the error, and try the next item...
                }
            }
            catch (Exception) { }

            if (hits != 0)
            {
                selectMail.SelectedIndex = 0;
            }
            error(hits + " mails for " + uname);
            return hits;
        }

        private void unpickle()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(startDir + "\\userdb.txt");
                while (file != null)
                {
                    string account = file.ReadLine();
                    if (account == null) break;
                    string pass = file.ReadLine();
                    selectUser.Items.Add(account);
                    users.Add(account, pass);
                }
                file.Close();
            }
            catch (Exception) { }
            selectUser.SelectedIndex=0;
        }

        private void checkMail(string user, string pass, string addr )
        {
            curUser = user;
            curPass = pass;
            try
            {
                sock = new TcpClient( addr, 110 );
                reader = new StreamReader(sock.GetStream());
                writer = new StreamWriter(sock.GetStream());
            }
            catch (Exception)
            {
                error( "could not connect to " + addr );
                return;
            }

            firstMail = getFirstMailOnlyToolStripMenuItem.Checked;
            delOnServer = deleteOnServerToolStripMenuItem.Checked;
            try
            {
                // this.backgroundWorker1.RunWorkerAsync();
                backgroundWorker1_DoWork(null, null);
                listDir(curUser);
            } catch(Exception ex) 
            {
                error(ex.ToString());
            }
        }

        private void pickle()
        {
            try
            {
                //System.IO.StreamWriter file = new System.IO.StreamWriter("userdb.txt");
                //for (int i = 0; i < users.Count; i++)
                //{
                //    User u = users[i];

                //    file.WriteLine(u.);
                //}
                //file.WriteLine("#");
                //file.Close();
            }
            catch (Exception) { }
        }

        private string find(string mess, string start, string stop)
        {
            int s0 = mess.IndexOf(start,StringComparison.InvariantCultureIgnoreCase);
            if (s0 < 0) return null;
            s0 += start.Length;

            int s1 = mess.Length;
            if ( stop != null )
                s1 = mess.IndexOf(stop, s0, StringComparison.InvariantCultureIgnoreCase);
            if (s1 < s0) return null;

            return mess.Substring(s0, s1 - s0);
        }

        private string readLine()
        {
            if (reader == null) return null;
            string response = reader.ReadLine();
            writeLog(response);
            return response;
        }

        private void writeLine(string mess, bool flush)
        {
            if (writer == null) return;

            int n = mess.Length;
            if (n > 128)
            {
                int start = 0;
                while (start < n - 128)
                {
                    writer.WriteLine(mess.Substring(start, 128));
                    start += 128;
                }
                int fin = n - start;
                writer.WriteLine(mess.Substring(start, fin));
            }
            else
                writer.WriteLine(mess);

            if (flush)
                writer.Flush();
            
            writeLog(mess);
        }

        private void writeLog(string mess)
        {
            if (logFile == null) return;
            logFile.WriteLine(mess);
            logFile.Flush();
        }

        private void writeLogDate(string mess)
        {
            string s = "none";
            try
            {
                s = DateTime.Now.ToString();
            }
            catch(Exception) {}

            writeLog(mess + " " + s);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            writeLogDate("get");
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Increment(5);
            if (!checkResponse(reader, "+OK"))
                return;
            writeLine("user " + curUser, true);
            toolStripProgressBar1.Increment(5);

            if (!checkResponse(reader, "+OK"))
                return;
            writeLine("pass " + curPass,true);
            toolStripProgressBar1.Increment(5);

            if (!checkResponse(reader, "+OK"))
                return;
            writeLine("stat"    ,true);
            toolStripProgressBar1.Increment(5);

            string response = readLine();
            int nMess = 0;
            if (response!=null)
                nMess = int.Parse(response.Split(' ')[1]);

            if (firstMail && (nMess>0))
                nMess = 1;

            for (int i = 0; i < nMess; i++)
            {
                int toolInc = 40 / nMess;
                writeLine("list " + (i + 1),true);
                response = readLine();
                int nBytes = int.Parse(response.Split(' ')[2]);
                int nRead = 0;
                writeLine("retr " + (i + 1), true);
                response = readLine();
                if (response.StartsWith("+OK"))
                {
                    string mess = "";
                    string line;
                    while (null != (line = readLine()))
                    {
                        //if (nRead > nBytes - 64)
                        if (line == ".")
                            break;
                        nRead += line.Length;
                        mess += line + "\r\n";
                    }
                    string subject = find(mess, "Subject: ", "\r\n");
                    if (subject != null)
                    {
                        int n = subject.Length;
                        if (n > 12) 
                            subject = subject.Substring(0, 12);
                        subject = nice(subject);
                    }
                    string from = find(mess, "Return-Path: ", "\r\n");
                    if (from == null)
                    {
                        from = find(mess, "Sender: ", "\r\n");
                    }
                    if (from == null)
                    {
                        from = find(mess, "From: ", "\r\n").Replace('\"', ' ');
                    }
                    if (from == null)
                    {
                        from = "(unknown)";
                    }
                    from = nice(from);
                    string date = find(mess, "Date: ", "\r\n");
                    if (date == null)
                    {
                        date = DateTime.Now.ToLongDateString();
                    }
                    date = nice(date);
                    string fn = startDir + "\\inbox\\" + curUser + "_" + from + "_" + subject + "_" + date + ".txt";
                    System.IO.StreamWriter file = new System.IO.StreamWriter(fn);
                    file.WriteLine(mess);
                    file.Close();
                    toolStripProgressBar1.Increment(toolInc);
                    writeLog("[finished "+fn+" ("+mess.Length+")after " + nRead + " / " + nBytes + "]");
                }
                if (delOnServer)
                {
                    writeLine("dele " + (i + 1), true);
                    if (!checkResponse(reader, "+OK"))
                        return;
                }
                toolStripProgressBar1.Increment(toolInc);
            }
            this.toolStripStatusLabel1.Text = nMess + " new messages for " + curUser;

            writeLine("quit", true);
        }


        //
        // generated callbacks:
        //

        void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 27)
            {
                this.Close();
                writeLogDate("bye.. ");
            }
        }

        void selectUser_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            writeLogDate("user " + selectUser.Text);
            listDir(selectUser.Text);
        }

        void selectMail_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            showMail( selectMail.Text, false );
        }

        private void buttonAttach_Click(object sender, EventArgs e)
        {
            DialogResult r = openFileDialog1.ShowDialog();
            mailAttach.Text = openFileDialog1.FileName;
        }


        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (!panel1.Visible)
            {
                webBrowser1.Visible = false;
                selectMail.Visible = false;
                panel1.Visible = true;
            }
            else
            {
                sendMail(selectUser.Text, mailTo.Text, mailSubj.Text, mailAttach.Text, mailText.Text);
            }
        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                webBrowser1.Visible = true;
                selectMail.Visible = true;
                panel1.Visible = false;
            }
            else
            {
                string[] ss = selectUser.Text.Split('@');
                string addr = "pop3." + ss[1];
                checkMail( ss[0], (string)users[selectUser.Text], addr );
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (!webBrowser1.Visible)
                return;
            if (selectMail.Text == "")
                return;
            File.Delete(startDir + "\\inbox\\" + selectMail.Text );
            listDir(selectUser.Text);
        }

        private void getFirstMailOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            firstMail = getFirstMailOnlyToolStripMenuItem.Checked;
        }

        private void deleteOnServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delOnServer = deleteOnServerToolStripMenuItem.Checked;
        }

        private void logTcpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((logFile == null) && (logTcpToolStripMenuItem.Checked))
            {
                logFile = new System.IO.StreamWriter(startDir + "\\log.txt",appendLog);
            }
            else
            {
                logFile.Close();
                logFile = null;
            }
            showLogToolStripMenuItem.Enabled = (logFile == null);
        }

        private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string lf = "";
            try
            {
                System.IO.StreamReader lfile = new System.IO.StreamReader(startDir + "\\log.txt");
                string line = null;
                if (lfile == null)
                    return;
                while ((line = lfile.ReadLine()) != null)
                {
                    if (line == "")
                        break;
                    lf += line+"\r\n";
                }
                lfile.Close();
            }
            catch (Exception) 
            {
                return;
            }

            panel1.Visible = false;
            selectMail.Visible = true;
            webBrowser1.Visible = true;
            webBrowser1.DocumentText = "<plaintext>" + lf;
        }

        private void appedLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            appendLog = appedLogToolStripMenuItem.Checked;
        }

 
        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Opacity = transparentToolStripMenuItem.Checked ? 0.45 : 1.0;
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            showMail(selectMail.Text, true);
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = startDir + "\\inbox\\" + toolStripStatusLabel2.Text;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
            catch (Exception) { }
        }

        private int countDir(string uname)
        {
            string key = uname.Split('@')[0];
            int hits = 0;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(startDir + "\\inbox");
                foreach (FileSystemInfo fsi in dir.GetFileSystemInfos())
                {
                    if (fsi is FileInfo)
                    {
                        FileInfo f = (FileInfo)fsi;
                        if (f.Name.StartsWith(key))
                        {
                            hits++;
                        }
                    }
                }
            }
            catch (Exception) { }
            return hits;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (string u in selectUser.Items)
            {
                int n = countDir(u);
                string[] ss = u.Split('@');
                checkMail(ss[0], (string)users[u], "pop3." + ss[1]);
            }
        }
   }
}
