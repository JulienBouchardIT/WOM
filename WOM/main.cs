using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace WOM
{
    class Program
    {

        public static void print(Object o)
        {
            Console.Out.WriteLine(o.ToString());
        }

        private static String GenOption(String param, String desc)
        {
            int spacing = 15;
            spacing= spacing-param.Length;
            String opt = "   "+param;
            for (int i = 0; i < spacing; i++)
            {
                opt+=" ";
            }
            return opt+desc;
        }

        /*static void Main(string[] args)
        {
            #if DEBUG
            //args = new string[] {"--killAll"};
            args = new string[] { "test123" };
            #endif

            if (args.Length < 1 || (args.Length == 1 && (args[0].Contains("help")|| args[0]=="-h")))
            {
                String pName = Process.GetCurrentProcess().ProcessName;
                print(pName+" {Option}");
                print("");
                print("Options:");
                print(GenOption("--killAll","Kill all windows between the background and icons"));
                print(GenOption("--fs", "Kill all windows between the background and icons"));

            }
            else if (args.Length == 1 && args[0] == "--killAll")
            {
                DesktopHandler.KillAll();
            }
            else if (args.Length == 2 && args[0] == "--killByName")
            {
                DesktopHandler.KillByName(args[1]);
            }
            else
            {
                IntPtr parent = DesktopHandler.GetDesktopHandler();
                IntPtr child = DesktopHandler.GetHandlerByName(args[0]);

                int left = 0, top = 0, w = 600, h = 600;
                if (args.Length == 2 && args[1] == "-fs")
                {
                    left = 0;
                    top = 0;
                    w = 1920;
                    h = 1080;
                }
                else if (args.Length == 3 && args[1] == "-fs")
                {
                    int windowId = Int32.Parse(args[2]);
                    left = 0;
                    top = 0;
                    w = 1920;
                    h = 1080;
                }

                W32.MoveWindow(child, left, top, w, h, true);

                IntPtr b = W32.SetParent(child, parent);

                uint resp = W32.GetLastError();
            }
        }*/
    }
}
