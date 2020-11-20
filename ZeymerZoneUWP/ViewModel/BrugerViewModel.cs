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
            _ = SetCurrent();
            LoginKnap = new RelayCommand(Setkunde);// instantiere relaycommands
            OpretKnap = new RelayCommand(Gemkunde);
            SletKnap = new RelayCommand(SletKunde);
            OpdaterKnap = new RelayCommand(OpdaterKundeAsync);
            
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
        public RelayCommand OpdaterKnap { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public DateTimeOffset FoedselsdagOffset { get; set; }
        public string FoedselsdagFormat
        {
            get
            {
                if (CurrentKunde.Kunde_foedeselsdag.Date.ToString("dd MMMM yyyy") == null) return "error";
                return CurrentKunde.Kunde_foedeselsdag.Date.ToString("dd MMMM yyyy");
            }
            set
            {
            }
        }

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
                    return;
                }
            }
        }
        /// <summary>
        /// en funktion til knappen som udfører setkunde metoden
        /// </summary>
        public void Setkunde()
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
        /// <summary>
        /// Bliver brugt til at sikre at current kunde altid er den gemte kunde på disken
        /// </summary>
        private async Task SetCurrent()
        {
            currentKunde = await PersistencyService<Kunde>.HentDataDisk("KundeCurrent");
           // if(tempkunde == null)
           // {

           // }
            //else
            //{
                
            //}
        }
        public async void OpdaterKundeAsync()
        {
            PersistencyService<Kunde>.UpdateData("kundes",CurrentKunde,CurrentKunde.Kunde_Id);
            await PersistencyService<Kunde>.GemDataDisk(CurrentKunde, "KundeCurrent");
        }
        public void SletKunde()
        {
            // load alle kostplaner
            ICollection<Kostplan> kostplaner = PersistencyService<ICollection<Kostplan>>.HentData("Kostplans").Result;
            foreach (var item in kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    PersistencyService<Kunde>.FjernData("Kostplans", item.Kostplan_Id);
                }
            }
            // slet nødvendige
            // load alle logs
            ICollection<Log> logs = PersistencyService<ICollection<Log>>.HentData("logs").Result;
            foreach (var item in logs)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    PersistencyService<Log>.FjernData("logs", item.Logs_Id);
                }
            }
            // slet nødvendige 
            // load alle konsultationer
            ICollection<Konsultation> konsultationer = PersistencyService<ICollection<Konsultation>>.HentData("konsultations").Result;
            foreach (var item in konsultationer)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    PersistencyService<Konsultation>.FjernData("konsultations", item.Konsultation_Id);
                }
            }
            // slet nødvendige 
            PersistencyService<Kunde>.FjernData("kundes",CurrentKunde.Kunde_Id);
        }

    }
}
