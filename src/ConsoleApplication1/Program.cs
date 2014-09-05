using FormInstantiationImplementation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string path = @"C:\Users\Alex\Google Drive\Monster Hunter\Athena Stuff\Athena's ASS MH3U 1.38b.exe";
            var x = new FiiV138bBridge(path);
            x.Launch();
            x.DoStuff();
            while (true)
            {
                
            }
        }
    }
}
