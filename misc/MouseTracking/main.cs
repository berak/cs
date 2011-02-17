using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace MouseTracking
{
    [Serializable]
    class MouseData
    {
        private int moved = 0;
        private int leftDown = 0;
        private int rightDown = 0;
        private int speedSum = 0;

        public MouseData()
        {
        }

        public void incMoved()
        {
            moved++;
        }

        public void addSpeed(int speed)
        {
            speedSum += speed;
        }

        public void incRightDown()
        {
            rightDown++;
        }

        public void incLeftDown()
        {
            leftDown++;
        }

        public int getMoved()
        {
            return moved;
        }
    }

    enum MouseMessages
    {
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_MOUSEMOVE = 0x0200,
        WM_MOUSEWHEEL = 0x020A,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205
    }

    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    class MouseTracker
    {
        private LowLevelMouseProc proc;
        private IntPtr hookID;
        private int[,] md;
        private const int WH_MOUSE_LL = 14;
        private Point oldPt;
        private Bitmap bmp;
        private bool _renderToDesk = false;
        private bool _refreshDesk = false;
        private double distance;
        private Rectangle desktopRect;

        public bool renderToDesk
        {
            get
            {
                return _renderToDesk;
            }
            set
            {
                _renderToDesk = value;
            }
        }

        public bool refreshDesk
        {
            get
            {
                return _refreshDesk;
            }
            set
            {
                _refreshDesk = value;
            }
        }

        #region DLL-Imports
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(
            int uAction, int uParam, string lpvParam, int fuWinIni);

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;
        #endregion

        public MouseTracker()
        {
            desktopRect = Screen.AllScreens[0].Bounds;
            for (int i = 1; i < Screen.AllScreens.Length; ++i)
            {
                desktopRect = Rectangle.Union(desktopRect, Screen.AllScreens[i].Bounds);
            }

            md = new int[desktopRect.Width, desktopRect.Height];
            oldPt = new Point(-1, -1);
            bmp = new Bitmap(desktopRect.Width, desktopRect.Height);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream f = new FileStream("data.dat", FileMode.Open, FileAccess.Read, FileShare.None);
                md = (int[,])formatter.Deserialize(f);
                distance = (double)formatter.Deserialize(f);
                f.Close();
            }
            catch (FileNotFoundException e)
            {
                for (int x = 0; x < desktopRect.Width; x++)
                    for (int y = 0; y < desktopRect.Height; y++)
                        md[x, y] = 0;
                distance = 0;
            }

            proc = HookCallback;
            hookID = SetHook(proc);
        }

        private void drawLine(Point pt)
        {
            if (oldPt.X == -1)
                oldPt = pt;

            int dx = pt.X - oldPt.X;
            int dy = pt.Y - oldPt.Y;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                for (int i = 0; i < Math.Abs(dx); i++)
                {
                    int newX = oldPt.X + i * Math.Sign(dx);
                    int newY = (int)(oldPt.Y + (double)i / (double)Math.Abs(dx) * dy);
                    if (desktopRect.Contains(new Point(newX, newY)) && md[newX - desktopRect.X, newY - desktopRect.Y] >= 0)
                        md[newX - desktopRect.X, newY - desktopRect.Y]++;
                }
            }
            else
            {
                for (int i = 0; i < Math.Abs(dy); i++)
                {
                    int newY = oldPt.Y + i * Math.Sign(dy);
                    int newX = (int)(oldPt.X + (double)i / (double)Math.Abs(dy) * dx);
                    if (desktopRect.Contains(new Point(newX, newY)) && md[newX - desktopRect.X, newY - desktopRect.Y] >= 0)
                        md[newX - desktopRect.X, newY - desktopRect.Y]++;
                }
            }

            oldPt = pt;
        }

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            ProcessModule curModule = Process.GetCurrentProcess().MainModule;
            return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            Point pt = new Point(hookStruct.pt.x, hookStruct.pt.y);

            if (nCode >= 0)
            {
                if (MouseMessages.WM_MOUSEMOVE == (MouseMessages)wParam)
                {
                    distance += Math.Sqrt(Math.Pow(pt.X - oldPt.X, 2) + Math.Pow(pt.Y - oldPt.Y, 2)) / 1000000.0;
                    MainClass.notifyIcon.Text = "MouseTracker: mouse moved over " + Math.Round(distance, 3) + " mpixels";
                    drawLine(pt);
                }
                else if (MouseMessages.WM_LBUTTONDOWN == (MouseMessages)wParam)
                {
                    if (desktopRect.Contains(pt)) 
                        md[pt.X - desktopRect.X, pt.Y - desktopRect.Y] = -1;
                }
                else if (MouseMessages.WM_RBUTTONDOWN == (MouseMessages)wParam)
                {
                    if (desktopRect.Contains(pt)) 
                        md[pt.X - desktopRect.X, pt.Y - desktopRect.Y] = -2;
                }
            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        public int[,] getMouseData()
        {
            return md;
        }

        public void Unhook()
        {
            UnhookWindowsHookEx(hookID);
        }

        public void Reset()
        {
            for (int x = 0; x < desktopRect.Width; x++)
            {
                for (int y = 0; y < desktopRect.Height; y++)
                {
                    md[x, y] = 0;
                }
            }
        }

        public double getDistance()
        {
            return distance;
        }

        public void Render()
        {
            do
            {
                double max = 0;

                for (int x = 0; x < desktopRect.Width; x++)
                {
                    for (int y = 0; y < desktopRect.Height; y++)
                    {
                        max = Math.Max(max, md[x, y]);
                    }
                }

                max = Math.Sqrt(max);

                BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, desktopRect.Width, desktopRect.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                int stride = bmData.Stride;
                System.IntPtr Scan0 = bmData.Scan0;
                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;
                    int nOffset = stride - desktopRect.Width * 3;

                    for (int y = 0; y < desktopRect.Height; y++)
                    {
                        for (int x = 0; x < desktopRect.Width; x++)
                        {
                            if (md[x, y] == -1)
                            {
                                p[0] = (byte)0;
                                p[1] = (byte)0;
                                p[2] = (byte)255;
                            }
                            else if (md[x, y] == -2)
                            {
                                p[0] = (byte)255;
                                p[1] = (byte)0;
                                p[2] = (byte)0;
                            }
                            else
                            {
                                byte color = (byte)(255.0 / max * Math.Sqrt(md[x, y]));

                                p[0] = p[1] = p[2] = color;
                            }
                            p += 3;
                        }
                        p += nOffset;

                        if (_renderToDesk)
                            Thread.Sleep(10);
                    }
                }

                bmp.UnlockBits(bmData);

                bmp.Save("Output.bmp", ImageFormat.Bmp);

                if (_renderToDesk)
                {
                    if (_refreshDesk)
                    {
                        SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, Directory.GetCurrentDirectory() + "\\Output.bmp", SPI_SETDESKWALLPAPER);
                       // WinAPI.SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, files[lastwallpaper], SPIF_SENDCHANGE);
                    }
                    Thread.Sleep(5000);
                }
            } while (_renderToDesk);
        }
    }

    class MainClass
    {
        public static NotifyIcon notifyIcon;   // static class members are evil!
        private static MouseTracker tracker;
        private static MenuItem menuDesk;
        private static Thread th;

        public static void Main(String[] args)
        {
            MenuItem menuClose = new MenuItem("Exit", new EventHandler(notifyIcon_Close));
            MenuItem menuRender = new MenuItem("Render", new EventHandler(notifyIcon_Render));
            MenuItem menuReset = new MenuItem("Reset", new EventHandler(notifyIcon_Reset));
            menuDesk = new MenuItem("Start render to Desktop", new EventHandler(notifyIcon_Desk));
            notifyIcon = new NotifyIcon();
            notifyIcon.Text = "MouseTracker";

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CHEMICAL.ico");
            notifyIcon.Icon = MouseTracking.Properties.Resources.CHEMICAL;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] { menuRender, menuDesk, menuReset, menuClose });
            notifyIcon.Visible = true;
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            tracker = new MouseTracker();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("-w"))
                    tracker.refreshDesk = true;
                else if (args[i].Equals("-r"))
                    notifyIcon_Desk(null, null);
            }
            

            ApplicationContext ap = new ApplicationContext();
            Application.Run(ap);
        }

        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            tracker.renderToDesk = false;
            FileStream f = new FileStream("data.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter b = new BinaryFormatter();
            b.Serialize(f, tracker.getMouseData());
            b.Serialize(f, tracker.getDistance());
            f.Close();

            notifyIcon.Visible = false;
            tracker.Unhook();
        }

        static void notifyIcon_Close(object sender, EventArgs e)
        {
            Application.Exit();
        }

        static void notifyIcon_Render(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(tracker.Render));
            th.Start();
        }

        static void notifyIcon_Desk(object sender, EventArgs e)
        {
            if (tracker.renderToDesk)
            {
                tracker.renderToDesk = false;
                menuDesk.Text = "Start render to Desktop";
                notifyIcon.ContextMenu.GetContextMenu().MenuItems[0].Visible = true;
            }
            else
            {
                th = new Thread(new ThreadStart(tracker.Render));
                th.Start();
                tracker.renderToDesk = true;
                menuDesk.Text = "Stop render to Desktop";
                notifyIcon.ContextMenu.GetContextMenu().MenuItems[0].Visible = false;
            }
        }

        static void notifyIcon_Reset(object sender, EventArgs e)
        {
            tracker.Reset();
        }
    }
}
