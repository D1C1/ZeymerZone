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
        public VejlederViewModel()
        {
            CurrentVejleder = new Vejleder();
            SetVejleder();
        }

        private static Vejleder _currentVejleder;

        public Vejleder CurrentVejleder
        {
            get { return _currentVejleder; }
            set { _currentVejleder = value; }
        }

        public ObservableCollection<Vejleder> OC_Vejledere { get; set; } = new ObservableCollection<Vejleder>();

        public ICollection<Vejleder> Vejledere { get; set; }

        public async void SetVejleder()
        {
            Vejledere = await PersistencyService<ICollection<Vejleder>>.HentData("vejleders");
            foreach (var item in Vejledere)
            {
                OC_Vejledere.Add(item);
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
