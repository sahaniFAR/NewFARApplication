
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
    public static class UserUtility
    {
        //  static string Baseurl = "http://localhost:5000/";

        static string Baseurl = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("ApiAddress");
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



        public static async Task<User> IsValidUser(string email, string password)
        {
            User user =null;
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
                    var result = JsonConvert.DeserializeObject<User>(userResponse);
                    user = result;
                }

            }

            
            return user;
        }
        public static void setObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
       public static T getObjectAsJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

    }
}
