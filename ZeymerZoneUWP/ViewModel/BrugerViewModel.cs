﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace ZeymerZoneUWP
{
    public class BrugerViewModel : INotifyPropertyChanged
    {
        private Kunde currentKunde;

        public BrugerViewModel()
        {
           // Username = "Herik45@Lortemail.dk"; // midletidigt data til at teste metode
           // Password = "Tester";// midletidigt data til at teste metode           
            LoginKnap = new RelayCommand(KnapSetkunde);// instantiere relaycommands
            SetCurrent();
        }
        public Kunde CurrentKunde
        {
            get 
            {
                if (currentKunde == null)
                {
                    //SetCurrent();// get current kunde
                    //retrieve the user file.
                    //load the user and return it!
                }
                return currentKunde;

            }
            set
            {
                currentKunde = value;
                NotifyPropertyChanged();
            }
        }
        public RelayCommand LoginKnap { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// En metode til at sætte den nuværende kunde og gemme den kunde som er logget ind til disken
        /// </summary>
        /// <param name="username">kundens username</param>
        /// <param name="password">kundens password</param>
        public async void SetKundeAsync(string username, string password)
        {
            ICollection<Kunde> Kunder = new List<Kunde>();
            Kunder = PersistencyService<ICollection<Kunde>>.HentData("kundes").Result;
            foreach (var item in Kunder)
            {
                if (item.Kunde_email == username && item.Password == password)
                {
                    await PersistencyService<Kunde>.GemDataDisk(item, "KundeCurrent");
                }
            }
        }
        /// <summary>
        /// en funktion til knappen som udfører setkunde metoden
        /// </summary>
        public void KnapSetkunde()
        {
            SetKundeAsync(Username, Password);
        }
        /// <summary>
        /// Henter en specifik kunde ud fra et ID
        /// </summary>
        /// <param name="kundeId">kundens ID</param>
        private void Hentkunde(int kundeId)
        {
            CurrentKunde = PersistencyService<Kunde>.HentData("kundes", kundeId).Result;

        }
        private async void SetCurrent()
        {
            Kunde tempkunde = await PersistencyService<Kunde>.HentDataDisk("KundeCurrent");
            if(tempkunde == null)
            {

            }
            else
            {
                currentKunde = tempkunde;
            }
        }
    }
}
