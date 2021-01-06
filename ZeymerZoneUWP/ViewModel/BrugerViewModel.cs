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
        private static Kunde _currentKunde = new Kunde();
        private string _status;
        private Kunde _tempKunde = new Kunde();
        private INavigationService _navigationService;

        public BrugerViewModel()
        {
            //_currentKunde = new Kunde();
            //_tempKunde = new Kunde();
            _navigationService = new NavigationService();
            SetCurrent();
            LoginKnap = new RelayCommand(Setkunde);// instantierer relaycommands
            OpretKnap = new RelayCommand(Gemkunde);
            SletKnap = new RelayCommand(SletKunde);
            OpdaterKnap = new RelayCommand(OpdaterKundeAsync);

        }

        #region Properties
        public string Status
        {
            get { return _status; }
            set { _status = value; NotifyPropertyChanged(); }
        }
        public Kunde CurrentKunde
        {
            get
            {
                return _currentKunde;
            }
            set
            {
                _currentKunde = value;
                NotifyPropertyChanged();
            }
        }
        public Kunde TempKunde
        {
            get
            {
                return _tempKunde;
            }
            set
            {
                _tempKunde = value;
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
        public DateTimeOffset FoedselsdagOffset { get; set; } = new DateTimeOffset(new DateTime(1980, 1, 1));
        public string FoedselsdagFormat
        {
            get
            {

                return $"{CurrentKunde.Kunde_foedeselsdag.Date.Day}/{CurrentKunde.Kunde_foedeselsdag.Date.Month}-{CurrentKunde.Kunde_foedeselsdag.Date.Year}";
            }            
        }
        #endregion





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
                    _navigationService.Navigate(typeof(MainPageLoggetInd));
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
        /// Gemmer currentkunde
        /// </summary>
        private void Gemkunde()
        {
            //CurrentKunde = new Kunde();
            TempKunde.Kunde_foedeselsdag = FoedselsdagOffset.DateTime;
            if (TempKunde.Password != RepeatPassword)
            {
                Status = "Password matcher ikke hinanden";
            }
            else if (TempKunde.Password == null)
            {
                Status = "Hovsa. Du SKAL lave et password";
            }
            else
            {
                PersistencyService<Kunde>.GemData("kundes", TempKunde);
            }

        }

        /// <summary>
        /// Bliver brugt til at sikre at current kunde altid er den gemte kunde på disken
        /// </summary>
        private async void SetCurrent()
        {
            CurrentKunde = await PersistencyService<Kunde>.HentDataDisk("KundeCurrent");
        }
        public async void OpdaterKundeAsync()
        {
            PersistencyService<Kunde>.UpdateData("kundes", CurrentKunde, CurrentKunde.Kunde_Id);
            await PersistencyService<Kunde>.GemDataDisk(CurrentKunde, "KundeCurrent");
        }

        /// <summary>
        /// Metode til at slette bruger fra database
        /// </summary>
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
            PersistencyService<Kunde>.FjernData("kundes", CurrentKunde.Kunde_Id);
        }

    }
}
