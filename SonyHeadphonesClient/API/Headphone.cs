using System.Runtime.InteropServices;
using System.Text;

namespace SonyHeadphonesClient.API
{
    internal class Headphone
    {
        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static void GetDevices(ref cross_Devices pData);

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static bool ConnectDevice(int pData);

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static void DisConnectDevice();

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static bool IsConnected();

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static void SetAmbientSoundControl(bool pData);

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static bool GetAmbientSoundControl();

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static int GetAsmLevel();

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static bool IsSetAsmLevelAvailable();

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static void SetAsmLevel(int pData);

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static void SetFocusOnVoice(bool pData);

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static bool SetChanges();

        [DllImport("SonyHeadphonesClientDll.dll")]
        public extern static bool IsChanged();
    }
    public struct cross_Devices
    {
        public struct Device
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            public char[] name;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
            public char[] mac;
        };

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public Device[] device;
    };
    public struct Devices
    {
        public string name;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
        public string mac;
        public int number;
        public string ConnectFailed;

        public Devices(char[] c_name, char[] c_mac, int c_number)
        {
            name = new string(c_name);
            mac = new string(c_mac);
            ConnectFailed = "Collapsed";

            name = name.Remove(name.IndexOf('\0'));

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            name = Encoding.UTF8.GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("GBK"), Encoding.UTF8.GetBytes(name)));
            number = c_number;
        }
        public Devices(cross_Devices.Device device, int c_number)
        {
            name = new string(device.name);
            mac = new string(device.mac);
            ConnectFailed = "Collapsed";

            name = name.Remove(name.IndexOf('\0'));

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            name = Encoding.UTF8.GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("GBK"), Encoding.UTF8.GetBytes(name)));
            number = c_number;
        }
    };
}
