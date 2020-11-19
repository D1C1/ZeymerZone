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
        private string _status;

        public BrugerViewModel()
        {
           // Username = "Herik45@Lortemail.dk"; // midletidigt data til at teste metode
           // Password = "Tester";// midletidigt data til at teste metode           
            LoginKnap = new RelayCommand(KnapSetkunde);// instantiere relaycommands
            OpretKnap = new RelayCommand(Gemkunde);
            SletKnap = new RelayCommand(SletKundeKnap);
            SetCurrent();
        }
        

        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged(); }
        }

        public Kunde CurrentKunde
        {
            get 
            {
                return currentKunde;
            }
            set
            {
                currentKunde = value;
                NotifyPropertyChanged();
            }
        }
        public RelayCommand LoginKnap { get; set; }
        public RelayCommand OpretKnap { get; set; }
        public RelayCommand SletKnap { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public DateTimeOffset FoedselsdagOffset { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// En metode til at sætte den nuværende kunde og gemme den kunde som er logget ind til disken
        /// </summary>
        /// <param name="username">kundens username</param>
        /// <param name="password">kundens password</param>
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
        /// <summary>
        /// en funktion til knappen som udfører setkunde metoden
        /// </summary>
        public void KnapSetkunde()
        {
            SetKundeAsync(Username, Password);
        }
        /// <summary>
        /// Henter en specifik kunde ud fra et ID
        /// </summary>
        /// <param name="kundeId">kundens ID</param>
        private void Hentkunde(int kundeId)
        {
            CurrentKunde = PersistencyService<Kunde>.HentData("kundes", kundeId).Result;

        }
        /// <summary>
        /// Gemmer currentkunde
        /// </summary>
        private void Gemkunde()
        {
            CurrentKunde.Kunde_foedeselsdag = FoedselsdagOffset.DateTime;
            if (CurrentKunde.Password != RepeatPassword)
            {
                Status = "Password matcher ikke hinanden";
            }
            else
            {
                PersistencyService<Kunde>.GemData("kundes", CurrentKunde);
            }
            
        }
        private async void SetCurrent()
        {
            Kunde tempkunde = await PersistencyService<Kunde>.HentDataDisk("KundeCurrent");
            if(tempkunde == null)
            {

            }
            else
            {
                currentKunde = tempkunde;
            }
        }
        public void SletKundeKnap()
        {
            PersistencyService<Kunde>.FjernData("kundes",CurrentKunde.Kunde_Id);
        }

    }
}
