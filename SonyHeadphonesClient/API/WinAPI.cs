using System;
using System.Runtime.InteropServices;

namespace SonyHeadphonesClient.API
{
    internal class WinAPI
    {
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        public const int SW_SHOW = 5;
        public const int SW_HIDE = 0;

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "ShowWindow")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetActiveWindow")]
        public static extern IntPtr GetActiveWindow();

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "GetDpiForWindow")]
        public static extern int GetDpiForWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public static double dpi
        {
            get
            {
                IntPtr hWnd = FindWindow("Shell_TrayWnd", null);
                int zoom = GetDpiForWindow(hWnd);
                double dpi = zoom / 96.0;
                return dpi;
            }
        }
    }
}
