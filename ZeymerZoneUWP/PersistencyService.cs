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
        const string serverUrl = "https://zzwebservice.azurewebsites.net/";
        
        /// <summary>
        /// Metode til at gemme data på database
        /// </summary>
        /// <param name="controllerNavn"></param>
        /// <param name="nyData"></param>
        public static void GemData(string controllerNavn, T nyData)
        {
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
                    var postResponse = client.PostAsJsonAsync<T>($"api/{controllerNavn}", nyData).Result;

                    //Check response -> throw exception if NOT successful
                    postResponse.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

        }

        /// <summary>
        /// Metode til at hente data fra database
        /// </summary>
        /// <param name="controllerNavn"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metode til at hente data fra specifik entry
        /// </summary>
        /// <param name="controllerNavn"></param>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Metode til at opdatere data på specifik entry
        /// </summary>
        /// <param name="controllerNavn"></param>
        /// <param name="dataToBeUpdated"></param>
        /// <param name="key"></param>
        public static void UpdateData(string controllerNavn,T dataToBeUpdated, int key)
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
                    var putResponse = client.PutAsJsonAsync<T>($"api/{controllerNavn}/{key}", dataToBeUpdated).Result;

                    //Check response -> throw exception if NOT successful
                    putResponse.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

        }

        /// <summary>
        /// Metode til at fjerne specifik entry
        /// </summary>
        /// <param name="controllerNavn"></param>
        /// <param name="key"></param>
        public static void FjernData(string controllerNavn,int key)
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
                    var deleteResponse = client.DeleteAsync($"api/{controllerNavn}/{key}").Result;

                    //Check response -> throw exception if NOT successful
                    deleteResponse.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        //lokal filer
        /// <summary>
        /// Metode til at gemme data lokalt
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filnavn"></param>
        /// <returns></returns>
        public static async Task GemDataDisk(T data, string filnavn)
        {
            string jsonText = JsonConvert.SerializeObject(data);
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localfolder.CreateFileAsync(filnavn, CreationCollisionOption.ReplaceExisting);

            await FileIO.WriteTextAsync(file, jsonText);
        }
        /// <summary>
        /// Metode til at hente data lokalt
        /// </summary>
        /// <param name="filnavn"></param>
        /// <returns></returns>
        public static async Task<T> HentDataDisk(string filnavn)
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localfolder.GetFileAsync(filnavn);
            string jsonText = await FileIO.ReadTextAsync(file);
            T tempData = JsonConvert.DeserializeObject<T>(jsonText);
            return tempData;
        }
        /// <summary>
        /// Metode til at lave fil lokalt til at gemme på/hente fra
        /// </summary>
        /// <param name="filNavn"></param>
        public static async void Makefile(string filNavn)
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localfolder.CreateFileAsync(filNavn, CreationCollisionOption.ReplaceExisting);
        }
    }
}
