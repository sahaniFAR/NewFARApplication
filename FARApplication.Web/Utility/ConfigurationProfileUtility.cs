
using FARApplication.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FARApplication.Web.Utility
{
    public static class ConfigurationProfileUtility
    {
       
        static string Baseurl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("ApiAddress");
        


        public static async Task<ConfigurationProfile> GetConfigurationProfileData()
        {
            ConfigurationProfile objconfigProfile = null;
            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url
                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient

                HttpResponseMessage Res = await httpClient.GetAsync("api/ConfigProfile/GetConfigData");

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var userResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    var result = JsonConvert.DeserializeObject<ConfigurationProfile>(userResponse);
                    objconfigProfile = result;
                }

            }


            return objconfigProfile;
        }
       

    }
}
