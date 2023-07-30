using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using SonyHeadphonesClient.API;
using System.Threading;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SonyHeadphonesClient.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        public SettingPage()
        {
            CancellationToken cancellationToken = tokenSource.Token;
            this.InitializeComponent();
            Task.Run(() =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    bool SetChangesSuccess = Headphone.SetChanges();
                    if (!SetChangesSuccess)
                        tokenSource.Cancel();
                }
                bool GoBackSuccess = DispatcherQueue.TryEnqueue(() => Frame.GoBack());
                Headphone.DisConnectDevice();
            }, cancellationToken);
            amsToggled(this.FindName("AmbientSoundControl"), null);
            amsSlider_ValueChanged(this.FindName("amsSlider"), null);
            FocusOnVoiceToggled(this.FindName("FocusOnVoice"), null);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            title.Text = e.Parameter.ToString();
        }
        private void amsSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (slider != null)
            {
                ToggleSwitch FocusOnVoice = this.FindName("FocusOnVoice") as ToggleSwitch;
                if (FocusOnVoice != null)
                {
                    if (slider.Value > 1)
                        FocusOnVoice.IsEnabled = true;
                    else
                        FocusOnVoice.IsEnabled = false;
                }
                Headphone.SetAsmLevel((int)slider.Value - 1);
            }
        }

        private void amsToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                Slider amsSlider = this.FindName("amsSlider") as Slider;
                ToggleSwitch FocusOnVoice = this.FindName("FocusOnVoice") as ToggleSwitch;
                if (toggleSwitch.IsOn == true)
                {
                    amsSlider.IsEnabled = true;
                    if (amsSlider.Value > 1)
                        FocusOnVoice.IsEnabled = true;
                    else
                        FocusOnVoice.IsEnabled = false;
                    Headphone.SetAmbientSoundControl(true);
                }
                else
                {
                    amsSlider.IsEnabled = false;
                    FocusOnVoice.IsEnabled = false;
                    Headphone.SetAmbientSoundControl(false);
                }
            }
        }
        private void FocusOnVoiceToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                    Headphone.SetFocusOnVoice(true);
                else
                    Headphone.SetFocusOnVoice(false);
            }
        }
        private void Disconnect(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
        }
    }
}
