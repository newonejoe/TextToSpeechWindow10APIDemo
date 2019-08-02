using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Windows.Media.SpeechSynthesis;

namespace TextToSpeechWindow10APIDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // speaker
        Windows.Media.SpeechSynthesis.SpeechSynthesizer speaker;

        // Void Dictionary
        Dictionary<string, VoiceInformation> voices;

        public MainWindow()
        {
            InitializeComponent();

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.tbInputText.Text == "")
            {
                Talk("There is no message to transfer, please input some sentense");
            }
            else
            {
                Talk(this.tbInputText.Text.Trim());
            }
        }

        private async void Talk(string message)
        {
            var stream = await speaker.SynthesizeTextToStreamAsync(message);
            (xamlhost.Child as Windows.UI.Xaml.Controls.MediaElement).SetSource(stream, stream.ContentType);
            (xamlhost.Child as Windows.UI.Xaml.Controls.MediaElement).Play();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            // Initial Speaker
            speaker = new SpeechSynthesizer();

            voices = new Dictionary<string, VoiceInformation>();

            // Add the voice to the listbox
            foreach (var item in SpeechSynthesizer.AllVoices)
            {
                this.voiceSelecter.Items.Add(item.DisplayName);
                voices.Add(item.DisplayName, item);
            }
        }

        private void VoiceSelecter_Selected(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WOW");
            //Windows.Media.SpeechSynthesis.SpeechSynthesizer.TrySetDefaultVoiceAsync();
        }

        private void VoiceSelecter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string voiceName = (e.AddedItems[0]) as string;
            speaker.Voice = voices[voiceName];
        }

        private void TbInputText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click(null, null);
            }
        }
    }
}
