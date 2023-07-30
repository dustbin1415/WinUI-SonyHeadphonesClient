using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using SonyHeadphonesClient.API;
using System;
using System.Diagnostics.Metrics;
using System.Threading;
using Windows.Graphics;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SonyHeadphonesClient
{
    public sealed partial class MainWindow : Window
    {
        public static IntPtr hwnd;
        private static AppWindow appWindow;
        private static int ScreenHeight;
        private static int ScreenWidth;
        public static bool IsShow = false;
        private static double dpi;

        public MainWindow()
        {
            dpi = WinAPI.dpi;
            this.InitializeComponent();

            //将窗口设置为菜单模式
            hwnd = WindowNative.GetWindowHandle(this);
            appWindow = AppWindow.GetFromWindowId(Win32Interop.GetWindowIdFromWindow(hwnd));
            appWindow.SetPresenter(OverlappedPresenter.CreateForContextMenu());
            WinAPI.SetWindowLong(hwnd, -20, 0x80);

            //重设圆角
            var attribute = WinAPI.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = WinAPI.DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
            WinAPI.DwmSetWindowAttribute(hwnd, attribute, ref preference, sizeof(uint));

            //定位窗口
            ScreenHeight = DisplayArea.Primary.WorkArea.Height;
            ScreenWidth = DisplayArea.Primary.WorkArea.Width;
            appWindow.Resize(new SizeInt32((int)(350 * dpi), (int)(280 * dpi)));
            appWindow.Move(new PointInt32(ScreenWidth - (int)((350 + 10) * dpi), ScreenHeight + 100));
        }
        public static void ShowWindow()
        {
            WinAPI.ShowWindow(hwnd, 1);
            for (int i = 280; i >= 0; i--)
            {
                appWindow.Move(new PointInt32(ScreenWidth - (int)((350 + 10) * dpi), ScreenHeight - (int)((10 + 280 - i) * dpi)));
                if (i % 80 == 0)
                    Thread.Sleep(1);
            }
            WinAPI.SetWindowPos(hwnd, -1, 0, 0, 0, 0, 1 | 2);
            IsShow = true;
        }

        public static void HideWindow()
        {
            WinAPI.SetWindowPos(hwnd, -1, 0, 0, 0, 0, 1 | 2);
            for (int i = 0; i <= 280; i++)
            {
                appWindow.Move(new PointInt32(ScreenWidth - (int)((350 + 10) * dpi), ScreenHeight - (int)((10 + 280 - i) * dpi)));
                if (i == 20)
                    WinAPI.SetWindowPos(hwnd, -2, 0, 0, 0, 0, 1 | 2);
                if (i % 80 == 0)
                    Thread.Sleep(1);
            }
            WinAPI.ShowWindow(hwnd, 0);
            Thread.Sleep(1000);
            IsShow = false;
        }
        void Current_Activated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState ==
                 WindowActivationState.Deactivated)
            {
                WinAPI.SetWindowPos(hwnd, -1, 0, 0, 0, 0, 1 | 2);
                Thread childThread = new Thread(HideWindow);
                childThread.Start();
            }
        }

    }
}
