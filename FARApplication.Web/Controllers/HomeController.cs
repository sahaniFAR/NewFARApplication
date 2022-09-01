//using FARApplication.Service.Controllers;
using FARApplication.Web.Models;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace FARApplication.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // private FarController _service;
        //Hosted web API REST Service base url
       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

        }


        public ActionResult Index()
        {
            List<FAR> FARInfo = new List<FAR>();
            var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
            if (user != null)
            {
                var userId = user.Id;
                if (user.ApprovalLevel == 0)
                {
                    ViewBag.Mode = "User";
                    FARInfo = FARUtility.GetAllFARs(userId).Result;
                }
                else
                {
                    ViewBag.Mode = "Admin";
                    FARInfo = FARUtility.GetALLFAR().Result;
                }

                if (FARInfo != null)
                {
                    FARInfo.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status; t.CreatedBy = string.Concat(t.User.FirstName, " ", t.User.LastName); });
                }
            }

            return View(FARInfo);
        }
        public ActionResult search(string search)
        {
            List<FAR> FARInfo = new List<FAR>();
            var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
            List<FAR> FARInforesult = new List<FAR>();
            if (user != null)
            {
                if (user.ApprovalLevel == 0)
                {
                    FARInfo = FARUtility.GetAllFARs(user.Id).Result;
                }
                else
                {
                    FARInfo = FARUtility.GetALLFAR().Result;
                }
                if(!string.IsNullOrEmpty(search))
                {
                    FARInforesult = FARInfo.ToList().Where(t => t.RequestId.Contains(search)).ToList();
                }

            }
            return View("Index", FARInforesult);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}