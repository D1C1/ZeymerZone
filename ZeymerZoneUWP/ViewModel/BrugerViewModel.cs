using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ZeymerZoneUWP
{
    public class BrugerViewModel : INotifyPropertyChanged
    {
        private Kunde currentKunde;
        //Ensure tha this is the same URL as applied under 
        //Properties->Web->Project URL in the web service project 
        const string serverUrl = "http://localhost:57648/";
        //Setup client handler
        HttpClientHandler handler = new HttpClientHandler();

        public BrugerViewModel()
        {
            handler.UseDefaultCredentials = true;
            HentKunde = new RelayCommand(Hentkunde);
        }
        public Kunde CurrentKunde
        {
            get { return currentKunde; }
            set
            {
                currentKunde = value;
                NotifyPropertyChanged();
            }
        }
        public RelayCommand HentKunde { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Hentkunde()
        {
            CurrentKunde = PersistencyService<Kunde>.HentData("kundes",1).Result;

        }
    }
}
