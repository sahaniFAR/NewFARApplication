﻿
using FARApplication.Web.Models;
using FARApplication.Web.Utility;
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
        public ActionResult Index(FAR model)
        {
            if(ModelState.IsValid)
            {
               // model.UserId = 1;
                model.Status = 1;
                string strFar = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(strFar, Encoding.UTF8, "application/json");
                var response = client.PostAsync(client.BaseAddress + "/FAR", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    ViewBag.SuccessResult = "New FAR created successfully!!";
                    return RedirectToAction("Index","Home");
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
            var FarRequestId = _iconfiguration["FARRequestId"];
            if (FarRequestId != null)
            {
                Far.RequestId = FarRequestId;
            }

            return View(Far);
        }
    }
}
