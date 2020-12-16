using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ZeymerZoneWebService;

namespace ZeymerZoneTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ensure tha this is the same URL as applied under 
            //Properties->Web->Project URL in the web service project 
            const string serverUrl = "https://zzwebservice.azurewebsites.net/";

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
                    //Get all the vejleders from the database
                    var getVejledersResponse = client.GetAsync("api/Vejleders").Result;

                    //Check response -> throw exception if NOT successful
                    getVejledersResponse.EnsureSuccessStatusCode();

                    //Get the vejleders as a IEnumerable
                    var vejleders = getVejledersResponse.Content.ReadAsAsync<ICollection<Vejleder>>().Result;

                    //List vejleders on the screen
                    foreach (var vejleder in vejleders)
                    {
                        Console.WriteLine(vejleder.Vejleder_navn);
                    }

                    //Prepare the next vejleders primary key
                    // is auto gened

                    //Create the new vejleder object
                    Vejleder newVejleder = new Vejleder()
                    {
                        Vejleder_navn = "Christoffer",
                        Vejleder_email = "chrstof@gmail.com",
                        Vejleder_tlfnr = "65987422"
                    };
                    Console.WriteLine("posting");
                    //Post the new vejleder to the database
                    var postResponse = client.PostAsJsonAsync<Vejleder>("api/Vejleders", newVejleder).Result;

                    //Check response -> throw exception if NOT successful
                    postResponse.EnsureSuccessStatusCode();
                    var vejlederTemp = postResponse.Content.ReadAsAsync<Vejleder>().Result;
                    Console.WriteLine("fetching");
                    //Fetch the vejleder from the database 
                    var getVejlederResponse = client.GetAsync($"api/Vejleders/{vejlederTemp.Vejleder_Id}").Result;

                    //Check response -> throw exception if NOT successful
                    getVejlederResponse.EnsureSuccessStatusCode();

                    //Update the vejleder object
                    Vejleder vejlederToBeUpdated = getVejlederResponse.Content.ReadAsAsync<Vejleder>().Result;
                    vejlederToBeUpdated.Vejleder_navn += " Update";
                    Console.WriteLine("putting");
                    //Put the updated vejleder object back into the database
                    var putResponse = client.PutAsJsonAsync<Vejleder>($"api/Vejleders/{vejlederToBeUpdated.Vejleder_Id}", vejlederToBeUpdated).Result;

                    //Check response -> throw exception if NOT successful
                    putResponse.EnsureSuccessStatusCode();

                    getVejlederResponse = client.GetAsync($"api/Vejleders/{vejlederToBeUpdated.Vejleder_Id}").Result;

                    //Check response -> throw exception if NOT successful
                    getVejlederResponse.EnsureSuccessStatusCode();
                    Console.WriteLine("deleting");
                    //Delete the vejleder object in the database
                    Vejleder VejlederToBeDeleted = getVejlederResponse.Content.ReadAsAsync<Vejleder>().Result;
                    var deleteResponse = client.DeleteAsync($"api/Vejleders/{VejlederToBeDeleted.Vejleder_Id}").Result;

                    //Check response -> throw exception if NOT successful
                    deleteResponse.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }
    }
}
