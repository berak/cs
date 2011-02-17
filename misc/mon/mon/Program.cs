using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;



namespace mon
{
    class Program
    {
        static void count()
        {
            PerformanceCounterCategory[] perfCounters = PerformanceCounterCategory.GetCategories();
            foreach (PerformanceCounterCategory pc in PerformanceCounterCategory.GetCategories())
            {
                System.Console.WriteLine("<" + pc.CategoryName + " type='" + pc.CategoryType+"'>");
                foreach (string i in pc.GetInstanceNames())
                    System.Console.WriteLine("<instance name='"+ i+"'/>");
                try
                {
                    foreach (PerformanceCounter p in pc.GetCounters())
                        System.Console.WriteLine("<counter name='"+ p.CounterName +"' type='"+ p.CounterType + "' help='" + p.CounterHelp +"'/>");
                }
                catch (Exception) { }
                System.Console.WriteLine("</" + pc.CategoryName + ">");
            }
        }
        
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "?")
            {
                count();
                return;
            }
            for (int i = 0; i < args.Length; i += 2)
            {
                string cat = args[i];
                string con = args[i+1];
                PerformanceCounter pc = new PerformanceCounter(cat, con);
                System.Console.WriteLine(pc.NextValue());
            }
        }
    }
}
