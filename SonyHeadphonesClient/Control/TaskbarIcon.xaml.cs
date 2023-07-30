using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using SonyHeadphonesClient.API;
using SonyHeadphonesClient.UICommand;
using System;
using System.Resources;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.ViewManagement;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SonyHeadphonesClient.Control
{
    public sealed partial class TaskbarIcon : UserControl
    {
        public ICommand LeftClickCommand_ = new LeftClickCommand();
        public ICommand ExitCommand_ = new ExitCommand();
        public ICommand AutoRuntCommand_;

        private ToggleMenuFlyoutItem AutoStartItem;

        public TaskbarIcon()
        {
            this.InitializeComponent();
            var Theme = Application.Current.RequestedTheme;
            Console.WriteLine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            if (Theme == ApplicationTheme.Light)
                TaskbarIconView.IconSource = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "LightIcon.ico"));
            else
                TaskbarIconView.IconSource = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "DarkIcon.ico"));
            AutoStartItem = FindName("AutoStart") as ToggleMenuFlyoutItem;
            AutoRuntCommand_ = new AutoRuntCommand(CheckAutoStart);
            CheckAutoStart();
        }
        public void CheckAutoStart()
        {
            if (AutoRun.IsAutoStart())
                AutoStartItem.IsChecked = true;
            else
                AutoStartItem.IsChecked = false;
        }
        public static bool IsAdministrator()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
