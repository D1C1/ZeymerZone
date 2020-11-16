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
            Username = "Herik45@Lortemail.dk";
            Password = "Tester";
            handler.UseDefaultCredentials = true;
            LoginKnap = new RelayCommand(KnapSetkunde);
        }
        public Kunde CurrentKunde
        {
            get 
            {
                if (currentKunde == null)
                {
                    CurrentKunde = PersistencyService<Kunde>.HentData("kundes", 1).Result;// get current kunde
                    //retrieve the configuration file.
                    //load the configuration and return it!
                }
                return currentKunde;

            }
            set
            {
                currentKunde = value;
                NotifyPropertyChanged();
            }
        }
        public RelayCommand LoginKnap { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void SetKundeAsync(string username, string password)
        {
            ICollection<Kunde> Kunder = new List<Kunde>();
            Kunder = PersistencyService<ICollection<Kunde>>.HentData("kundes").Result;
            foreach (var item in Kunder)
            {
                if (item.Kunde_email == username && item.Password == password)
                {
                    await PersistencyService<Kunde>.GemDataDisk(item, "KundeCurrent");
                }
            }
        }
        public void KnapSetkunde()
        {
            SetKundeAsync(Username, Password);
        }
        private void Hentkunde()
        {
            CurrentKunde = PersistencyService<Kunde>.HentData("kundes",1).Result;

        }
    }
}
