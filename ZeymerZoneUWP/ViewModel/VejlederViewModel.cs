using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ZeymerZoneUWP
{
    public class VejlederViewModel : INotifyPropertyChanged
    {
        public static Vejleder _currentVejleder;
        private Kunde _newKunde;

        public VejlederViewModel()
        {            
            SetVejleder();
            SetKunder();
            LoadLogKnap = new RelayCommand(SetLogs);
        }
        public ObservableCollection<Vejleder> OC_Vejledere { get; set; } = new ObservableCollection<Vejleder>();
        public ICollection<Vejleder> Vejledere { get; set; }
        public ObservableCollection<Kunde> OC_Kunder { get; set; } = new ObservableCollection<Kunde>();
        public ICollection<Kunde> Kunder { get; set; }
        public ObservableCollection<Log> OC_Logs { get; set; } = new ObservableCollection<Log>();
        public ICollection<Log> Logs { get; set; }
        public RelayCommand LoadLogKnap { get; set; }


        public Kunde NewKunde
        {
            get { return _newKunde; }
            set { _newKunde = value; }
        }
       

        public string KundesWeight
        {
            get { return $"Vægt: {SelectedLog.Kunde_vaegt_dd}"; }
            
        }


        private Log _selectedLog;

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
            Logs = await PersistencyService<ICollection<Log>>.HentData("logs");
            OC_Logs.Clear();
            foreach (var item in Logs)
            {
                if(NewKunde.Kunde_Id == item.Kunde_Id)
                {
                    OC_Logs.Add(item);
                }
            }
        }






        #region PropertyChanged Implementering
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
