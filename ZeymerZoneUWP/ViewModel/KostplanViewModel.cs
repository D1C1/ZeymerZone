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

        public static Kunde _currentKunde;
        private Kostplan selectedKostplan;

        public KostplanViewModel()
        {
            CurrentKunde = new Kunde();
            SetCurrent();
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
            SetKostplan();
        }

        #endregion

        public ObservableCollection<Kostplan> OC_Kostplaner { get; set; } = new ObservableCollection<Kostplan>();
        public Kostplan SelectedKostplan { 
            get 
            { 
                 return selectedKostplan;
            } 
            set 
            { 
                selectedKostplan = value;
                NotifyPropertyChanged();
            } 
        }

        /// <summary>
        /// En metode til at sætte den nuværende kundes kostplaner.
        /// </summary>
        public async void SetKostplan()
        {
            ICollection<Kostplan> Kostplaner = new List<Kostplan>();
            Kostplaner = await PersistencyService<ICollection<Kostplan>>.HentData("kostplans");
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
