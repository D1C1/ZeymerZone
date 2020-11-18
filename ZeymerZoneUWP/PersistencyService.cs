using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ZeymerZoneUWP
{
    public class PersistencyService<T>
    {
        //Ensure tha this is the same URL as applied under 
        //Properties->Web->Project URL in the web service project 
        //databasefiler
        const string serverUrl = "http://localhost:57648/";
        public static Task<T> HentData(string controllerNavn)
        {
            
            //Setup client handler
            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

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
                    //Get the data from the database
                    var getVejledersResponse = client.GetAsync($"api/{controllerNavn}/").Result;

                    //Check response -> throw exception if NOT successful
                    getVejledersResponse.EnsureSuccessStatusCode();


                    //Get the data as a IEnumerable
                    return getVejledersResponse.Content.ReadAsAsync<T>();


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

        }
        public static Task<T> HentData(string controllerNavn, int key)
        {
            //Setup client handler
            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

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
                    //Get the data from the database
                    var getVejledersResponse = client.GetAsync($"api/{controllerNavn}/{key}").Result;

                    //Check response -> throw exception if NOT successful
                    getVejledersResponse.EnsureSuccessStatusCode();


                    //Get the data as a IEnumerable
                    return getVejledersResponse.Content.ReadAsAsync<T>();

                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            
        }
        //lokal filer
        public static async Task GemDataDisk(T data, string filnavn)
        {
            string jsonText = JsonConvert.SerializeObject(data);
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localfolder.CreateFileAsync(filnavn, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, jsonText);
        }
        public static async Task<T> HentDataDisk(string filnavn)
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localfolder.GetFileAsync(filnavn);
            string jsonText = await FileIO.ReadTextAsync(file);
            T tempData = JsonConvert.DeserializeObject<T>(jsonText);
            return tempData;
        }
        public static async void Makefile(string filNavn)
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localfolder.CreateFileAsync(filNavn, CreationCollisionOption.ReplaceExisting);
        }
    }
}
