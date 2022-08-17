using FARApplication.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FARApplication.Web.Utility
{
    public static class FARUtility
    {
        static string Baseurl = "http://localhost:5000/";
        public static async Task<List<FAR>> GetAllFARs()
        {
            List<FAR> Fars = new List<FAR>();

            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url
                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await httpClient.GetAsync("api/FAR");

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    Fars = JsonConvert.DeserializeObject<List<FAR>>(Response);
                   
                }

            }
            return Fars;
        }
    }
}
