
using FARApplication.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public static async Task<bool> UpdateConfigurationProfile(ConfigurationProfile objConfig)
        {
            ConfigurationProfile objCP = objConfig;
            bool result = false;
            if (objCP != null)
            {
                string strFar = System.Text.Json.JsonSerializer.Serialize(objCP);

                StringContent content = new StringContent(strFar, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    //Define request data format
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to add web api REST service resource FAR using HttpClient
                    HttpResponseMessage Res = await httpClient.PutAsync(httpClient.BaseAddress + "api/ConfigProfile/Update", content);
                    if (Res.IsSuccessStatusCode)
                        return result = true;

                }
            }

            return result;
        }





    }
}
