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

        public LogViewModel()
        {
            SetCurrent();
            NewLog = new Log();
            ShowLogs = new RelayCommand(GetLogs);
        }

        public RelayCommand ShowLogs { get; set; }

        public Log NewLog { get; set; }

        public ObservableCollection<Log> KundeLogs { get; set; } = new ObservableCollection<Log>();

        private void GetLogs()
        {
            KundeLogs = PersistencyService<ObservableCollection<Log>>.HentData("logs").Result;


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
        }

        #endregion
    }
}
