using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using System.Timers;
using SonyHeadphonesClient.Control;
using SonyHeadphonesClient.API;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SonyHeadphonesClient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class ConnectPage : Page
    {
        public ConnectPage()
        {
            this.InitializeComponent();
            UpdateDevices();
        }

        private void UpdateDevices()
        {
            cross_Devices warp = new cross_Devices();
            try 
            {
                Headphone.GetDevices(ref warp);
            }
            catch
            {
                DevicesList.Visibility = Visibility.Collapsed;
                BluetoothError.Visibility = Visibility.Visible;
                return;
            }
            BluetoothError.Visibility = Visibility.Collapsed;
            DevicesList.Visibility = Visibility.Visible;
            DevicesList.Items.Clear();
            for (int i = 0; i < 1024; i++)
            {
                Devices _device = new Devices(warp.device[i], i);
                if (_device.name != "")
                    DevicesList.Items.Add(_device);
            }
            if (DevicesList.Items.Count == 0)
                NoDevices.Visibility = Visibility.Visible;
            else
                NoDevices.Visibility = Visibility.Collapsed;
        }
        private void UpdateDevices(object sender, RoutedEventArgs e)
        {
            UpdateDevices();
        }
        private void DeviceChoose(object sender, ItemClickEventArgs e)
        {
            Devices device;
            if (e.ClickedItem != null)
            {
                device = (Devices)e.ClickedItem;
                bool tmp = Headphone.ConnectDevice(device.number);
                if (tmp)
                {
                    Frame.Navigate(typeof(SettingPage),
                                   device.name,
                                   new SlideNavigationTransitionInfo()
                                   { Effect = SlideNavigationTransitionEffect.FromRight });
                }
                else
                {
                    device.ConnectFailed = "Visible";
                    ListView listView = sender as ListView;
                    listView.Items[listView.Items.IndexOf(e.ClickedItem)] = device;
                }
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateDevices();
        }
    }
}
