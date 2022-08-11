
using FARApplication.Web.Models;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Controllers
{
    public class FARController : Controller
    {
        private readonly ILogger<FARController> _logger;
        // private FarController _service;
        //Hosted web API REST Service base url
        string Baseurl = "http://localhost:1648/";
        public FARController(ILogger<FARController> logger)
        {
            _logger = logger;
        }
        public ActionResult Index()
        {
            FAR Far = new FAR();
            Far.CreatedOn = System.DateTime.Now;
            var CreatedBy = UserUtility.GetUserFullNameById(1);
            return View(Far);
        }
    }
}
