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
            AllDays = new RelayCommand(SetKostplanAllDays);
        }

        public RelayCommand Monday { get; set; }
        public RelayCommand Tuesday { get; set; }
        public RelayCommand Wednesday { get; set; }
        public RelayCommand Thursday { get; set; }
        public RelayCommand Friday { get; set; }
        public RelayCommand Saturday { get; set; }
        public RelayCommand Sunday { get; set; }
        public RelayCommand AllDays { get; set; }


        

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
        /// En metode til at hente alle kostplaner.
        /// </summary>
        public async void SetKostplan()
        {
            Kostplaner = await PersistencyService<ICollection<Kostplan>>.HentData("kostplans");          
        }

        public ICollection<Kostplan> Kostplaner { get; set; }

        #region SetKostplanDays
        /// <summary>
        /// Metoder til at hente kostplaner til nuværende kunde
        /// </summary>
        public void SetKostplanAllDays()
        {
            OC_Kostplaner.Clear();
            foreach (var item in Kostplaner)
            {
                if(item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    OC_Kostplaner.Add(item);
                }
            }
        }
        /// <summary>
        /// Metode til at hente kostplaner fra specifik dag
        /// </summary>
        /// <param name="day"></param>
        public void SetKostplanDay(string day)
        {
            OC_Kostplaner.Clear();
            /*
            var kostplanset = from k in Kostplaner
                              where k.Ugedag == day
                              select k;
            List<Kostplan> newlist = kostplanset.ToList();
            */
            List<Kostplan> newlist = Kostplaner.ToList();         

            foreach (var item in newlist.FindAll(k => k.Kunde_Id == CurrentKunde.Kunde_Id && k.Ugedag == day))
            {   
                    OC_Kostplaner.Add(item);                    
            }
        }

        /// <summary>
        /// TIL ALLE METODER NEDAD:
        /// Henter kostplaner fra specifik ugedag
        /// </summary>
        public void SetKostplanMonday()
        {
            SetKostplanDay("Mandag");
        }

        public void SetKostplanTuesday()
        {
            SetKostplanDay("Tirsdag");
        }
        public void SetKostplanWednesday()
        {
            SetKostplanDay("Onsdag");
        }

        public void SetKostplanThursday()
        {
            SetKostplanDay("Torsdag");
        }

        public void SetKostplanFriday()
        {
            SetKostplanDay("Fredag");
        }

        public void SetKostplanSaturday()
        {
            SetKostplanDay("Lørdag");
        }

        public void SetKostplanSunday()
        {
            SetKostplanDay("Søndag");
        }

        #endregion

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
            SetKostplanAllDays();
        }

        #endregion
    }
}
