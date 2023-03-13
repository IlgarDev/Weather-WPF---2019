using System;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Threading;

namespace Weather.Views
{
    public partial class SplashScreen : Window
    {
        SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public SplashScreen()
        {
            InitializeComponent();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            dispatcherTimer.Stop();
            this.Close();
        }
    }
}
