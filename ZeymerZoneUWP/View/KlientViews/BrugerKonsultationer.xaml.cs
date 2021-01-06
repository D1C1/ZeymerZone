using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZeymerZoneUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BrugerKonsultationer : Page
    {
        public KonsultationViewModel KonsultationVM  { get; set; }
        public BrugerKonsultationer()
        {
            KonsultationVM = new KonsultationViewModel();
            this.InitializeComponent();
        }
        private void Button_Click_Profil(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Profilside));
        }

        private void Button_Click_BrugerKostplan(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BrugerKostplan));
        }

        private void Button_Click_BrugerLogs(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BrugerLogs));
        }

        private void Button_Click_MainPageLoggetInd(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPageLoggetInd));
        }
        private void Button_Click_BrugerKonsultationer(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BrugerKonsultationer));
        }

        private void Button_Click_logud(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
