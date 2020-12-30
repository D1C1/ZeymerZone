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
    public class KonsultationViewModel
    {
        private Kunde _currentKunde;

        public KonsultationViewModel()
        {
            SetCurrent();
            
        }
        public ICollection<Konsultation> Konsultationer { get; set; }
        public ICollection<Vejleder> Vejledere { get; set; }
        public ObservableCollection<Konsultation> OC_Konsultationer { get; set; } = new ObservableCollection<Konsultation>();
        public ObservableCollection<Vejleder> OC_Vejledere { get; set; } = new ObservableCollection<Vejleder>();

        
        /// <summary>
        /// Henter vejledere og konsultationer som passer til CurrentKunde
        /// </summary>
        private void GetData()
        {
            Konsultationer = PersistencyService<ICollection<Konsultation>>.HentData("konsultations").Result;
            Vejledere = PersistencyService<ICollection<Vejleder>>.HentData("vejleders").Result;
            foreach (var item in Konsultationer)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    foreach (var item2 in Vejledere)
                    {
                        if (item.Vejleder_Id == item2.Vejleder_Id)
                        {
                            item.Vejleder = item2;
                        }
                    }
                    OC_Konsultationer.Add(item);
                }
            }
            foreach (var item in Vejledere)
            {
                OC_Vejledere.Add(item);
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

        /// <summary>
        /// Henter kunde som er logget ind fra disk og sætter til CurrentKunde
        /// </summary>
        private async void SetCurrent()
        {
            CurrentKunde = await PersistencyService<Kunde>.HentDataDisk("KundeCurrent");
            GetData();
        }

        #endregion
    }
}
