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
        public ObservableCollection<Konsultation> OC_Konsultationer { get; set; }
        public ObservableCollection<Konsultation> OC_Vejledere { get; set; }

        private void GetKonsultations()
        {
            Konsultationer = PersistencyService<ICollection<Konsultation>>.HentData("konsultations").Result;
            foreach (var item in Konsultationer)
            {
                if (item.Kunde_Id == CurrentKunde.Kunde_Id)
                {
                    OC_Konsultationer.Add(item);
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
        }

        #endregion
    }
}
