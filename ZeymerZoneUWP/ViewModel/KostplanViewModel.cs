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
        private Kostplan _selectedKostplan;

        public KostplanViewModel()
        {
            CurrentKunde = new Kunde();
            SetCurrent();
            Monday = new RelayCommand(SetKostplanMonday);
            Tuesday = new RelayCommand(SetKostplanTuesday);
            Wednesday = new RelayCommand(SetKostplanWednesday);
            Thursday = new RelayCommand(SetKostplanThursday);
            Friday = new RelayCommand(SetKostplanFriday);
            Saturday = new RelayCommand(SetKostplanSaturday);
            Sunday = new RelayCommand(SetKostplanSunday);
        }

        public RelayCommand Monday { get; set; }
        public RelayCommand Tuesday { get; set; }
        public RelayCommand Wednesday { get; set; }
        public RelayCommand Thursday { get; set; }
        public RelayCommand Friday { get; set; }
        public RelayCommand Saturday { get; set; }
        public RelayCommand Sunday { get; set; }


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
        public Kostplan SelectedKostplan
        {
            get
            {
                return _selectedKostplan;
            }
            set
            {
                _selectedKostplan = value;

            }
        }

        /// <summary>
        /// En metode til at sætte den nuværende kundes kostplaner.
        /// </summary>
        public async void SetKostplan()
        {

            Kostplaner = await PersistencyService<ICollection<Kostplan>>.HentData("kostplans");

        }

        public ICollection<Kostplan> Kostplaner { get; set; }

        #region SetKostplanDays
        public void SetKostplanMonday()
        {
            OC_Kostplaner.Clear();
            foreach (var item in Kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    if (item.Ugedag == "Mandag")
                    {
                        OC_Kostplaner.Add(item);
                    }
                }
            }
        }

        public void SetKostplanTuesday()
        {
            OC_Kostplaner.Clear();
            foreach (var item in Kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    if (item.Ugedag == "Tirsdag")
                    {
                        OC_Kostplaner.Add(item);
                    }
                }
            }
        }
        public void SetKostplanWednesday()
        {
            OC_Kostplaner.Clear();
            foreach (var item in Kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    if (item.Ugedag == "Onsdag")
                    {
                        OC_Kostplaner.Add(item);
                    }
                }
            }
        }

        public void SetKostplanThursday()
        {
            OC_Kostplaner.Clear();
            foreach (var item in Kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    if (item.Ugedag == "Torsdag")
                    {
                        OC_Kostplaner.Add(item);
                    }
                }
            }
        }

        public void SetKostplanFriday()
        {
            OC_Kostplaner.Clear();
            foreach (var item in Kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    if (item.Ugedag == "Fredag")
                    {
                        OC_Kostplaner.Add(item);
                    }
                }
            }
        }

        public void SetKostplanSaturday()
        {
            OC_Kostplaner.Clear();
            foreach (var item in Kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    if (item.Ugedag == "Lørdag")
                    {
                        OC_Kostplaner.Add(item);
                    }
                }
            }
        }

        public void SetKostplanSunday()
        {
            OC_Kostplaner.Clear();
            foreach (var item in Kostplaner)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    if (item.Ugedag == "Søndag")
                    {
                        OC_Kostplaner.Add(item);
                    }
                }
            }
        }

        #endregion
    }
}
