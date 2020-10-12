using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;

namespace WOM
{
    /**
     * 
     */
    class W32
    {
        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0,
            SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8
        }

        //Source: https://www.pinvoke.net/default.aspx/Enums/DeviceContextValues.html
        [Flags()]
        public enum DeviceContextValues : uint
        {
            Window = 0x00000001,
            Cache = 0x00000002,
            NoResetAttrs = 0x00000004,
            ClipChildren = 0x00000008,
            ClipSiblings = 0x00000010,
            ParentClip = 0x00000020,
            ExcludeRgn = 0x00000040,
            IntersectRgn = 0x00000080,
            ExcludeUpdate = 0x00000100,
            IntersectUpdate = 0x00000200,
            LockWindowUpdate = 0x00000400,
            UseStyle = 0x00010000,
            Validate = 0x00200000,
        }

        [DllImport("User32.Dll", SetLastError = true)]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.Dll", SetLastError = true)]
        public static extern int GetClassName(int hwnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr windowHandle, uint Msg, IntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags flags, uint timeout, out IntPtr result);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern Int32 SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern Boolean EnumChildWindows(int hWndParent, Delegate lpEnumFunc, int lParam);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern Int32 GetWindowText(int hWnd, StringBuilder s, int nMaxCount);

        [DllImport("User32.dll", SetLastError = true)]
        public static extern Int32 GetWindowTextLength(int hwnd);

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern int GetDesktopWindow();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        // Delegate to filter which windows to include 
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string lclassName, string windowTitle);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextValues flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern string SendMessage(int hWnd, int msg, string wParam, IntPtr lParam);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint GetLastError();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32")]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lpRect, MonitorEnumProc callback, int dwData);

        public delegate bool MonitorEnumProc(IntPtr hDesktop, IntPtr hdc, ref Rect pRect, int dwData);

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll")]
        public static extern bool CloseWindow(IntPtr hWnd);

        public const int WM_CLOSE = 0x0010;


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

    }
}
