
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
                var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
                if (user != null)
                {
                    model.UserId = user.Id;

                    model.Status = 1;
                    if (postedfiles != null)
                    {
                        Random random = new Random(1);
                        string fileLastName = random.Next().ToString();
                        if (!string.IsNullOrEmpty(fileLastName))
                            model.Filename = string.Concat(fileLastName, '_', postedfiles.FileName);
                    }
                    var EventModel = FARUtility.PrepareEventLog("Created");
                    model.FAREventLogs.Add(EventModel);
                    string strFar = JsonSerializer.Serialize(model);
                    StringContent content = new StringContent(strFar, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(client.BaseAddress + "/FAR/Add", content).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        ViewBag.SuccessResult = "New FAR created successfully!!";
                        return RedirectToAction("Index", "Home");


                    }
                }
                else
                {
                    ViewBag.SuccessResult = "Your session has expired!!";
                }
               
            }
            ViewBag.SuccessResult= "There is an error to create FAR!!";
            return View();
        }

        public ActionResult Index()
          {
            FAR Far = new FAR();
            Far.CreatedOn = System.DateTime.Now;
            var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
            
            if (user != null)
            {
                Far.CreatedBy = string.Concat(user.FirstName, " ", user.LastName);
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
        public ActionResult GetFARResult(int FARId) 
        {
            FAR far = new FAR();
            far = FARUtility.GetFARDetails(FARId).Result;
            if (far != null)
            {
                far.CreatedBy = string.Concat(far.User.FirstName, " ", far.User.LastName);
            }
            return View(far);
        }
        [HttpPost]
        public ActionResult Update(FAR far , string Mode)
        {
            if (!string.IsNullOrEmpty(Mode))
            {
                FAREventLog farEventog = new FAREventLog() { EventDate = System.DateTime.Now };
                var strApproverFullName = UserUtility.GetUserFullNameById(far.Approverdetails[0].UserId).Result;
                switch (Mode)
                {
                    case "Approve":
                        far.Status = 3;
                        farEventog.Message = String.Format("First level approved by{0}",strApproverFullName);
                        break;
                    case "Reject":
                        far.Status = 5;
                        farEventog.Message = String.Format("Rejected by{0}", strApproverFullName);
                        break;
                }
               
            }

            return View("index", far);
        }
    }
}
