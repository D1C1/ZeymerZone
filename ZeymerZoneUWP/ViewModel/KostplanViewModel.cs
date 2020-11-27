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
    public class KostplanViewModel : INotifyPropertyChanged
    {

        private Kunde _currentKunde;

        public KostplanViewModel()
        {
            SetCurrent();
            SetKostplan();
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

        public ObservableCollection<Kostplan> OC_Kostplaner { get; set; } = new ObservableCollection<Kostplan>();
        public Kostplan SelectedKostplan { get; set; }

        /// <summary>
        /// En metode til at sætte den nuværende kundes kostplaner.
        /// </summary>
        public void SetKostplan()
        {
            ICollection<Kostplan> Kostplaner = new List<Kostplan>();
            Kostplaner = PersistencyService<ICollection<Kostplan>>.HentData("kostplans").Result;
            foreach (var item in Kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    OC_Kostplaner.Add(item);
                }
            }
        }

    }
}
