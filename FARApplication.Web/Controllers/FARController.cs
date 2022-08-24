
using FARApplication.Web.Models;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace FARApplication.Web.Controllers
{
    public class FARController : Controller
    {
        private readonly ILogger<FARController> _logger;
        private IConfiguration _iconfiguration;
        Uri uri;
        IConfiguration configuration;
        HttpClient client;
        // private FarController _service;
        //Hosted web API REST Service base url

        public FARController(ILogger<FARController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _iconfiguration = configuration;
            uri = new Uri(configuration["ApiAddress"]);
            client = new HttpClient();
            client.BaseAddress = uri;
        }
        [HttpPost]
        public ActionResult Index(FAR model, IFormFile postedfiles)
        {
            if(ModelState.IsValid)
            {
               // model.UserId = 1;
                model.Status = 1;
                if (postedfiles != null)
                { 
                    Random random = new Random(1);
                    string fileLastName = random.Next().ToString();
                    if(!string.IsNullOrEmpty(fileLastName))
                    model.Filename = string.Concat(fileLastName, '_', postedfiles.FileName);
                }
                string strFar = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(strFar, Encoding.UTF8, "application/json");
                var response = client.PostAsync(client.BaseAddress + "/FAR/Add", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var EventModel = FARUtility.PrepareEventLog(5005, "Created");
                    string strFarEvent = JsonSerializer.Serialize(EventModel);
                    StringContent logcontent = new StringContent(strFarEvent, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(client.BaseAddress + "/FAREventLog", logcontent).Result;
                    if(result.IsSuccessStatusCode)
                    {
                        ViewBag.SuccessResult = "New FAR created successfully!!";
                        return RedirectToAction("Index", "Home");
                    }
                   
                }
               
            }
            ViewBag.SuccessResult = "New FAR created successfully!!";
            return View();
        }

            public ActionResult Index()
        {
            FAR Far = new FAR();
            Far.CreatedOn = System.DateTime.Now;
            //Far.CreatedBy = 
            var CreatedBy = UserUtility.GetUserFullNameById(1);
            if (CreatedBy != null)
            {
                Far.CreatedBy = CreatedBy.Result;
            }
            //var FarRequestId = _iconfiguration["FARRequestId"];
            var FarRequestId = FARUtility.GetSequeceForRequestId().Result;
            if (string.IsNullOrEmpty(FarRequestId))
            {
                FarRequestId = _iconfiguration["FARRequestId"];
            }

            if (FarRequestId != null)
            {
                var dateformat = System.DateTime.Now.ToString("yyyy-MM-dd");
                int sequence = Convert.ToInt32(FarRequestId) + 1;
                FarRequestId = dateformat + "-" + sequence.ToString().PadLeft(6, '0');
                Far.RequestId = FarRequestId;
            }

            return View(Far);
        }
    }
}
