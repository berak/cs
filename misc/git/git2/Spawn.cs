using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

using System.Net;
namespace Spawn
{
    public class SpawnCallback
    {
        public static string EXHAUSTED = " took too long!";
        public static string output = "";
        public static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            output += outLine.Data;
        }
        public static String spawn(String exe, String code, String args, String workdir)
        {
            SpawnCallback.output = "";
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = exe;
                p.StartInfo.Arguments = args;
                p.StartInfo.WorkingDirectory = workdir;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.OutputDataReceived += new DataReceivedEventHandler(SpawnCallback.OutputHandler);
                p.ErrorDataReceived += new DataReceivedEventHandler(SpawnCallback.OutputHandler);
                p.Start();
                p.BeginOutputReadLine();
                p.BeginErrorReadLine();

                if (code != null) // code might run from cmdline args
                {
                    StreamWriter win = p.StandardInput;
                    win.Write(code);
                    Console.WriteLine("spawn " + exe + " " + code);
                    win.Flush();
                    win.Close();
                }

                p.WaitForExit(5 * 1000); // don't loop me!
                if (p.HasExited == false)
                {
                    p.Kill();
                    SpawnCallback.output = "'" + code + "'" + EXHAUSTED;
                }
                p.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("error " + e.ToString() + " " + e.StackTrace);
            }
            Console.WriteLine("RES " + output);
            return SpawnCallback.output;
        }
    };

}
