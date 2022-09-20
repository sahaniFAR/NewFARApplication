using FARApplication.Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FARApplication.Web.Utility
{
    public static class FARUtility
    {
       
        static string Baseurl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("ApiAddress");
        public static async Task<List<FAR>> GetAllFARs(int userId)
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
                HttpResponseMessage Res = await httpClient.GetAsync("api/FAR/Get?userId=" + userId);

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
        public static async Task<string> GetSequeceForRequestId()
        {
            string strsequenceForRequestId = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url
                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await httpClient.GetAsync("api/FAR/GetRequestId");

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    strsequenceForRequestId = JsonConvert.DeserializeObject<string>(Response);

                }
                return strsequenceForRequestId;

            }
           
        }
        public static FAREventLog PrepareEventLog(string message)
        {
            FAREventLog model = new FAREventLog() { Message = message, EventDate= System.DateTime.Now };
            return model;
        }
        public static async Task<FAR> GetFARDetails(int FARId)
        {
            FAR Far = new FAR();

            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url
                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await httpClient.GetAsync("api/FAR/GetFARDetails?FARId=" + FARId);

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    Far = JsonConvert.DeserializeObject<FAR>(Response);

                }

            }
            return Far;
        }
        public static async Task<List<FAR>> GetALLFAR()
        {
            List<FAR> Fars = new List<FAR>();

            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url

                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllFAR using HttpClient
                HttpResponseMessage Res = await httpClient.GetAsync("api/FAR/GetAllFAR");

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
        public static async Task<bool> AddFar(FAR newFAR)
        {
            FAR objFAR = newFAR;
            bool result = false;
            if (objFAR != null)
            {
                string strFar = System.Text.Json.JsonSerializer.Serialize(objFAR);
                StringContent content = new StringContent(strFar, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    //Define request data format
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to add web api REST service resource FAR using HttpClient
                    HttpResponseMessage Res = await httpClient.PostAsync(httpClient.BaseAddress + "api/FAR/Add", content);
                    if (Res.IsSuccessStatusCode)
                        return result = true;

                }
            }

            return result;
        }
        public static async Task<bool> UpdateFar(FAR newFAR)
        {
            FAR objFAR = newFAR;
            bool result = false;
            if (objFAR != null)
            {
                string strFar = System.Text.Json.JsonSerializer.Serialize(objFAR);
                StringContent content = new StringContent(strFar, Encoding.UTF8, "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Baseurl);
                    httpClient.DefaultRequestHeaders.Clear();
                    //Define request data format
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to add web api REST service resource FAR using HttpClient
                    HttpResponseMessage Res = await httpClient.PutAsync(httpClient.BaseAddress + "api/FAR/Update", content);
                    if (Res.IsSuccessStatusCode)
                        return result = true;

                }
            }

            return result;
        }


        public static MemoryStream DownloadAttachedFile(string fileName, string uploadPath)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, fileName);
            var retunrcontent = new System.IO.MemoryStream();
            using (var memory = new MemoryStream())
            {
                if (System.IO.File.Exists(path))
                {
                    var net = new System.Net.WebClient();
                    var data = net.DownloadData(path);
                    var content = new System.IO.MemoryStream(data);
                    retunrcontent = content;
                }

                return retunrcontent;
            }
        }

    }
}
