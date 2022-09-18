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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FARApplication.Web.Controllers
{
    public class FARController : Controller
    {
        private readonly ILogger<FARController> _logger;
        private IConfiguration _iconfiguration;
        private readonly IWebHostEnvironment hostingEnvironment;
        Uri uri;
        IConfiguration configuration;
        HttpClient client;
        // private FarController _service;
        //Hosted web API REST Service base url

        public FARController(ILogger<FARController> logger, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _iconfiguration = configuration;
            this.hostingEnvironment = hostingEnvironment;
            uri = new Uri(configuration["ApiAddress"]);
            client = new HttpClient();
            client.BaseAddress = uri;
        }
        [HttpPost]
        public ActionResult Index(FAR model, IFormFile postedFiles, string command)
        {
            string strLogMessage = string.Empty;
            string strUserName = string.Empty;
            string uploadedFileName = null;
            if (ModelState.IsValid)
            {
                // model.UserId = 1;
                var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
                if (user != null)
                {
                    model.UserId = user.Id;

                    if (string.Compare(command, "Submit") == 0)
                    {
                        model.Status = 2;
                        strUserName = string.Concat(user.FirstName, " ", user.LastName);
                        strLogMessage = string.Format("Send for approval by {0}", strUserName);
                    }
                    else
                    {
                        model.Status = 1;
                        strUserName = string.Concat(user.FirstName, " ", user.LastName);
                        strLogMessage = string.Format("Created/modified by {0}", strUserName);
                    }
                    if (postedFiles != null)
                    {
                        string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "UploadedFile");
                        uploadedFileName = Guid.NewGuid().ToString() + "_" + postedFiles.FileName;
                        string filePath = Path.Combine(uploadsFolder, uploadedFileName);
                        postedFiles.CopyTo(new FileStream(filePath, FileMode.Create));
                    }
                    if (!string.IsNullOrEmpty(uploadedFileName))
                    {
                        model.Filename = uploadedFileName;
                    }

                    var EventModel = FARUtility.PrepareEventLog(strLogMessage);
                    model.FAREventLogs.Add(EventModel);
                    var result = FARUtility.AddFar(model).Result;

                    if (result)
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
                Far.User = user;
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

        [EncryptedActionParameterAttribute]
        public ActionResult GetFARResult(int FARId) 
        {
            FAR far = new FAR();
            var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
                if(user != null)
                {
                    ViewBag.SessionUser = user;
                }
           
                far = FARUtility.GetFARDetails(FARId).Result;
                if (far != null)
                {
                   // far.CreatedBy = string.Concat(far.User.FirstName, " ", far.User.LastName);
                    far.LifeCycleStatus = (DocumentStatus)far.Status;
                   FARApprover FARApprover = new FARApprover() { FARId = far.Id, UserId = user.Id, ApprovedDate = System.DateTime.Now };
                if (user != null)
                {
                    if ((int)user.ApprovalLevel == 0)
                    {

                        if ((int)far.Status == 1 || (int)far.Status == 5)
                        {
                            ViewBag.Mode = "User";
                            ViewBag.UserType = "User";
                        }
                    }

                    if ((int)user.ApprovalLevel == 1)
                    {
                        if ((int)far.Status == 2)
                        {
                            ViewBag.Mode = "Admin";
                            ViewBag.UserType = "Approver1";
                            if (far.Approverdetails.Count ==0)
                            far.Approverdetails.Add(FARApprover);
                        }
                    }

                    if ((int)user.ApprovalLevel == 2)
                    {
                        if ((int)far.Status == 3)
                        {
                            ViewBag.Mode = "Admin";
                            ViewBag.UserType = "Approver2";
                            if (far.Approverdetails.Count == 1)
                                far.Approverdetails.Add(FARApprover);
                        }
                    }

                }

            }

            return View(far);

        }
        [HttpPost]
        public ActionResult Update(FAR far , string Mode)
        {

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Mode))
                {
                   
                    string strMessage = string.Empty;
                    string strUserName = string.Empty;
     
                    var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
                    if (user != null)
                    {
                        strUserName = string.Concat(user.FirstName, " ", user.LastName);

                    }

                    switch (Mode)
                    {
                        case "Approve":
                            far.Status = far.Status + 1;
                            strMessage = user.ApprovalLevel == Level.FirstLevel?  String.Format("First level approved by {0}", strUserName): String.Format("Final level approved by {0}", strUserName);
                            break;

                        case "Reject":
                            far.Status = 5;
                            strMessage = String.Format("Rejected by {0}", strUserName);
                            break;
                        case "Submit":
                            far.Status = 2;
                            strMessage = String.Format("Send for approval {0}", strUserName);
                            break;
                        case "Save":
                            strMessage = String.Format("Modified By {0}", strUserName);
                            break;
                        case "SaveClose":
                            strMessage = String.Format("Modified By {0}", strUserName);
                            break;

                    }

                    FAREventLog farEventog = FARUtility.PrepareEventLog(strMessage);
                    farEventog.FARId = far.Id;
                    far.FAREventLogs.Add(farEventog);
                    var result = FARUtility.UpdateFar(far).Result;
                    if(result)
                    { 

                        ViewBag.SuccessResult = "FAR modified successfully!!";
                        return RedirectToAction("Index", "Home");
                    }

                    else
                    {
                        ViewBag.ErrorMsg = "There is some issue to update the record";
                    }

                }
            }

             return View();
        }
    }
}
