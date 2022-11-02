using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using FARApplication.Web.Models;

namespace FARApplication.Web.Utility
{
    public static class SearchUtility
    {
        static string Baseurl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("ApiAddress");
        public static async Task<FARViewModel> GetAllFAROnStatus(int statusId, int pageIndex)
        {
            FARViewModel Fars = new FARViewModel();

            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url

                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await httpClient.GetAsync("api/Search/GetFAROnStatus?status=" + statusId +"&pageIndex=" + pageIndex);

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    Fars = JsonConvert.DeserializeObject<FARViewModel>(Response);

                }

            }
            return Fars;
        }
        public static async Task<FARViewModel> GetAllFARBasedOnSubmiter(int submiterId, int pageIndex)
        {
            FARViewModel Fars = new FARViewModel();

            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url

                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await httpClient.GetAsync("api/Search/GetAllFARBasedOnSubmiter?submiterId=" + submiterId + "&pageIndex=" + pageIndex);

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    Fars = JsonConvert.DeserializeObject<FARViewModel>(Response);

                }

            }
            return Fars;
        }
    }
}

