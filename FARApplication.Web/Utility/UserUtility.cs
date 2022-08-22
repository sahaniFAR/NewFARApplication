
using FARApplication.Web.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FARApplication.Web.Utility
{
    public static class UserUtility
    {

        static string Baseurl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("ApiAddress");


        //static string Baseurl = "http://localhost:1648/";
        public static async Task<string> GetUserFullNameById(int Id)
        {
            string userName = string.Empty;
            
            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url
                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await httpClient.GetAsync("api/User/GetUserById/GetUserById?Id=" + Id);

                if(Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var userResponse= Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    var user = JsonConvert.DeserializeObject<User>(userResponse);
                    userName = user.FirstName + " " +  user.LastName;
                }

            }
                return userName;
        }



        public static async Task<bool> IsValidUser(string email, string password)
        {
            bool IsvalidUser = false;
            using (HttpClient httpClient = new HttpClient())
            {
                //Passing service base url
                httpClient.BaseAddress = new Uri(Baseurl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient

                HttpResponseMessage Res = await httpClient.GetAsync("api/User/ValidateUser?email="+email+"&password="+password);

                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var userResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    var isvalid = JsonConvert.DeserializeObject<bool>(userResponse);
                    IsvalidUser = isvalid;
                }

            }

            
            return IsvalidUser;
        }
    

    }
}
