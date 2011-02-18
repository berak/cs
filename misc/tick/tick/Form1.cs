using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace tick
{
    public partial class Form1 : Form
    {
        [DllImport("winmm.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        static extern bool PlaySound( string pszSound,IntPtr hMod,SoundFlags sf);

        [Flags]
        public enum SoundFlags : int
        {
            SND_SYNC = 0x0000,  // play synchronously (default) 
            SND_ASYNC = 0x0001,  // play asynchronously 
            SND_NODEFAULT = 0x0002,  // silence (!default) if sound not found 
            SND_MEMORY = 0x0004,  // pszSound points to a memory file
            SND_LOOP = 0x0008,  // loop the sound until next sndPlaySound 
            SND_NOSTOP = 0x0010,  // don't stop any currently playing sound 
            SND_NOWAIT = 0x00002000, // don't wait if the driver is busy 
            SND_ALIAS = 0x00010000, // name is a registry alias 
            SND_ALIAS_ID = 0x00110000, // alias is a predefined ID
            SND_FILENAME = 0x00020000, // name is file name 
            SND_RESOURCE = 0x00040004  // name is resource name or atom 
        }

        public Form1()
        {
            InitializeComponent();
            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);
        }

        void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(null,null);
            }
            if (e.KeyChar == 27)
            {
                this.Close();
            }
        }

        private void start()
        {
            timer1.Start();
            this.button1.Text = "stop!";
        }

        private void stop()
        {
            timer1.Stop();
            this.button1.Text = "go!";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = Int32.Parse(textBox1.Text);
            if (i == 0)
            {
                System.Console.Beep(200, 600);
                System.Console.Beep(400, 600);
                System.Console.Beep(600, 600);
                System.Console.Beep(800, 600);
                PlaySound("mgnmpi.wav", IntPtr.Zero, SoundFlags.SND_ASYNC | SoundFlags.SND_FILENAME);
                stop();
            }
            else
            {
                textBox1.Text = "" + (--i);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                return;
            if (this.button1.Text == "go!")
            {
                start();
            }
            else 
            {
                stop();
            }
        }
    }
}
