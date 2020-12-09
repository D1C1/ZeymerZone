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
    public class LogViewModel : INotifyPropertyChanged
    {
        private Kunde _currentKunde;
        private Log newLog;


        public LogViewModel()
        {
            SetCurrent();
            NewLog = new Log();
            ShowLogsKnap = new RelayCommand(GetLogs);
            GemLogKnap = new RelayCommand(GemLog);
            ShowAllLogsKnap = new RelayCommand(GetAllLogs);            
        }

        public RelayCommand ShowLogsKnap { get; set; }
        public RelayCommand ShowAllLogsKnap { get; set; }
        public RelayCommand GemLogKnap { get; set; }

        public Log NewLog
        {
            get { return newLog; }
            set { newLog = value; }
        }

        private void GemLog()
        {
            NewLog.Kunde_Id = CurrentKunde.Kunde_Id;
            NewLog.Vejleder_Id = 1;
            NewLog.Log_date = DateTime.Now;
            NewLog.Kunde_vaegt_dd = CurrentKunde.Kunde_vaegt;
            PersistencyService<Log>.GemData("logs", NewLog);
        }

        public DateTimeOffset CompareDate { get; set; }

        public ObservableCollection<Log> OC_KundeLogs { get; set; } = new ObservableCollection<Log>();
        public ICollection<Log> KundeLogs { get; set; }

        private void GetAllLogs()
        {
            KundeLogs = PersistencyService<ICollection<Log>>.HentData("logs").Result;
            OC_KundeLogs.Clear();
            foreach (var item in KundeLogs)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    OC_KundeLogs.Add(item);
                }
            }
        }

        private void GetLogs()
        {
            KundeLogs = PersistencyService<ICollection<Log>>.HentData("logs").Result;
            OC_KundeLogs.Clear();
            foreach (var item in KundeLogs)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    if (CompareDate != null)
                    {
                        if (item.Log_date == CompareDate.Date)
                        {
                            OC_KundeLogs.Add(item);
                        }
                    }
                    else
                    {
                        OC_KundeLogs.Add(item);
                    }
                }
            }
        }



        #region Henter kunde
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private async void SetCurrent()
        {
            CurrentKunde = await PersistencyService<Kunde>.HentDataDisk("KundeCurrent");
            GetAllLogs();
        }

        #endregion
    }
}
