using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WOM
{
    class DesktopHandler
    {

        public static IList<WinInterface> GetAllWindows()
        {
            List<WinInterface> windows = new List<WinInterface>();
            foreach (Process process in Process.GetProcesses())
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle) && 
                    W32.IsWindowVisible(process.MainWindowHandle) &&
                    !(process.ProcessName == "WOM")) 
                    windows.Add(new WinInterface(process));
            }

            return windows;
        }

        public static void KillAll()
        {
            IntPtr hwnd = GetDesktopHandler();
            W32.SendMessage(hwnd, W32.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        public static void KillByName(String name)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(name))
                {
                    pList.Kill();
                }
            }
        }

        public static void KillById(IntPtr id)
        {
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.Id == id.ToInt64())
                {
                    pList.Kill();
                }
            }
        }

        public static IntPtr GetHandlerByName(String name)
        {

            IntPtr hWnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(name))
                {
                    hWnd = pList.MainWindowHandle;
                }
            }
            return hWnd;
        }

        private static bool FindWorkerCallBack(IntPtr tophandle, IntPtr resivedWorker)
        {
            IntPtr workerw = resivedWorker;

            if (W32.FindWindowEx(tophandle, IntPtr.Zero, "SHELLDLL_DefView", "0") == IntPtr.Zero)
                return true;//Didnot found SHELL...

            // Get the WorkerW
            workerw = W32.FindWindowEx(IntPtr.Zero, tophandle, "WorkerW", "0");

            if (workerw == IntPtr.Zero)
                return true;

            return false;
        }

        // Source: https://www.codeproject.com/Articles/856020/Draw-Behind-Desktop-Icons-in-Windows-plus
        public static IntPtr GetDesktopHandler()
        {
            // Fetch the Progman window

            int progmanINT = W32.FindWindow("Progman", null);
            IntPtr progman = new IntPtr(progmanINT);//working

            IntPtr result = IntPtr.Zero;

            W32.SendMessage(progman, //Process to send msg to
                            0x052C, //Message send
                            new IntPtr(0),
                            new IntPtr(0));

            // We enumerate all Windows, until we find one, that has the SHELLDLL_DefView 
            // as a child. 
            // If we found that window, we take its next sibling and assign it to workerw.
            IntPtr workerw = IntPtr.Zero;
            W32.EnumWindows(new W32.EnumWindowsProc((tophandle, topparamhandle) =>
            {
                IntPtr p = W32.FindWindowEx(tophandle,
                                            IntPtr.Zero,
                                            "SHELLDLL_DefView",
                                            null);

                if (p != IntPtr.Zero)
                {
                    // Gets the WorkerW Window after the current one.
                    workerw = W32.FindWindowEx(IntPtr.Zero,
                                               tophandle,
                                               "WorkerW",
                                               null);
                }

                return true;
            }), IntPtr.Zero);

            return workerw;
        }
    }
}
