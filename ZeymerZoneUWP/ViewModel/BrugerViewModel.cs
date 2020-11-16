using System;
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
        //Ensure tha this is the same URL as applied under 
        //Properties->Web->Project URL in the web service project 
        const string serverUrl = "http://localhost:57648/";
        //Setup client handler
        HttpClientHandler handler = new HttpClientHandler();

        public BrugerViewModel()
        {
            handler.UseDefaultCredentials = true;
            HentKunde = new RelayCommand(Hentkunde);
        }
        public Kunde CurrentKunde
        {
            get { return currentKunde; }
            set
            {
                currentKunde = value;
                NotifyPropertyChanged();
            }
        }
        public RelayCommand HentKunde { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Hentkunde()
        {
            using (var client = new HttpClient(handler))
            {
                //Initialize client
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();

                //Request JSON format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    Console.WriteLine("getting");
                    //Get all the vejleders from the database
                    var getVejledersResponse = client.GetAsync("api/Kundes/1").Result;

                    //Check response -> throw exception if NOT successful
                    getVejledersResponse.EnsureSuccessStatusCode();
                    

                    //Get the vejleders as a IEnumerable
                    var kunde = getVejledersResponse.Content.ReadAsAsync<Kunde>().Result;

                    //List vejleders on the screen
                    CurrentKunde = kunde;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
