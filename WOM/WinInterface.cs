using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOM
{
    class WinInterface
    {
        public WinInterface(System.Diagnostics.Process process)
        {
            this.process = process;
            this.id = process.Id;
            this.name = process.ProcessName;
            this.title = process.MainWindowTitle;
            this.fix = false;
            this.handler = process.MainWindowHandle;
            this.isIcon = true;
        }

        public WinInterface()
        {
            this.id = 0;
            this.name = "DESKTOP ICONS";
            this.title = "DESKTOP ICONS";
            this.fix = true;
            this.isIcon = false;
        }

        public void addAsChild(IntPtr child)
        {
            W32.SetParent(child, this.handler);
        }

        public void setForeground()
        {
            Console.WriteLine("setForeground "+this.name);
            W32.SetForegroundWindow(this.handler);
        }

        public void Kill()
        {
            this.process.Kill();
        }

        public bool isIcon { get; }
        public int id { get; }
        public string name { get; }
        public string title { get; }
        public bool fix { get; set; }
        public IntPtr handler { get; }

        private System.Diagnostics.Process process { get; set; }
    }
}
