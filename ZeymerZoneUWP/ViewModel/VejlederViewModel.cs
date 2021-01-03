using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace ZeymerZoneUWP
{
    public class VejlederViewModel : INotifyPropertyChanged
    {
        public static Vejleder _currentVejleder;
        public static Kunde _newKunde;
        private Log _selectedLog;


        public VejlederViewModel()
        {
            SetVejleder();
            SetKunder();
            LoadLogKnap = new RelayCommand(SetLogs);
            KonsultationKnap = new RelayCommand(SetKonsultationer);
            OpretKonsultation = new RelayCommand(GemKonsultation);
            tomKnap = new RelayCommand(tom, CanClick);

        }
        #region Properties
        public ObservableCollection<Vejleder> OC_Vejledere { get; set; } = new ObservableCollection<Vejleder>();
        public ICollection<Vejleder> Vejledere { get; set; }
        public ObservableCollection<Kunde> OC_Kunder { get; set; } = new ObservableCollection<Kunde>();
        public ICollection<Kunde> Kunder { get; set; }
        public ObservableCollection<Log> OC_Logs { get; set; } = new ObservableCollection<Log>();
        public ICollection<Log> Logs { get; set; }
        public RelayCommand LoadLogKnap { get; set; }
        public ICollection<Konsultation> Konsultationer { get; set; }
        public ObservableCollection<Konsultation> OC_Konsultationer { get; set; } = new ObservableCollection<Konsultation>();
        public RelayCommand KonsultationKnap { get; set; }
        public Konsultation NewKonsultation { get; set; } = new Konsultation();
        public RelayCommand OpretKonsultation { get; set; }
        public RelayCommand tomKnap { get; set; }
        public DateTimeOffset NewKonDateDate { get; set; } = new DateTimeOffset(DateTime.Now);
        public string Timer { get; set; }
        public string Minutter { get; set; }
        public Kunde NewKunde
        {
            get { return _newKunde; }
            set
            {
                _newKunde = value;
                tomKnap.RaiseCanExecuteChanged();
            }
        }
        public string KundesWeight
        {
            get { return $"Vægt: {SelectedLog.Kunde_vaegt_dd}"; }

        }
        public Log SelectedLog
        {
            get { return _selectedLog; }
            set { _selectedLog = value; }
        }
        public Vejleder CurrentVejleder
        {
            get { return _currentVejleder; }
            set { _currentVejleder = value; }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Henter alle vejledere til OC
        /// </summary>
        public async void SetVejleder()
        {
            Vejledere = await PersistencyService<ICollection<Vejleder>>.HentData("vejleders");
            foreach (var item in Vejledere)
            {
                OC_Vejledere.Add(item);
            }
        }

        /// <summary>
        /// Henter alle kunder til OC
        /// </summary>
        public async void SetKunder()
        {
            Kunder = await PersistencyService<ICollection<Kunde>>.HentData("kundes");
            foreach (var item in Kunder)
            {
                OC_Kunder.Add(item);
            }
        }

        /// <summary>
        /// Henter alle logs til valgte kunde
        /// </summary>
        public async void SetLogs()
        {
            try
            {
                Logs = await PersistencyService<ICollection<Log>>.HentData("logs");
                OC_Logs.Clear();
                foreach (var item in Logs)
                {
                    if (NewKunde.Kunde_Id == item.Kunde_Id)
                    {
                        OC_Logs.Add(item);
                    }
                }
            }
            catch (NullReferenceException)
            {
            }
        }
        public void tom()
        {

        }
        private bool CanClick() { return NewKunde != null; }

        #endregion
        #region Konsultationer

        public async void SetKonsultationer()
        {
            Konsultationer = await PersistencyService<ICollection<Konsultation>>.HentData("konsultations");
            OC_Konsultationer.Clear();
            foreach (var item in Konsultationer)
            {
                if (NewKunde.Kunde_Id == item.Kunde_Id)
                {
                    OC_Konsultationer.Add(item);
                }
            }
        }

        public void GemKonsultation()
        {
            NewKonsultation.Kunde_Id = NewKunde.Kunde_Id;
            NewKonsultation.Vejleder_Id = CurrentVejleder.Vejleder_Id;
            NewKonsultation.Konsultation_date = NewKonDateDate.ToString("MM/dd/yyyy") + $" {Timer}:{Minutter}";
            PersistencyService<Konsultation>.GemData("konsultations", NewKonsultation);
        }


        #endregion





        #region PropertyChanged Implementering
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
