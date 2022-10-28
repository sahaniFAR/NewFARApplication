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

        [HttpPost]
        public ActionResult PageSearch(string status, string mode)
        {
            return View();
        }


        public ActionResult Index()
        {
            List<FAR> FARInfo = new List<FAR>();
            var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
            if (user != null)
            {
                ViewBag.SessionUser = user;
                var userId = user.Id;
                if (user.ApprovalLevel == 0)
                {
                    ViewBag.Mode = "User";
                    FARInfo = FARUtility.GetAllFARs(userId).Result;
                }
                else
                {
                    ViewBag.Mode = "Admin";
                    FARInfo = FARUtility. GetALLFAR().Result;
                }

                if (FARInfo != null)
                {
                    // FARInfo.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status; t.CreatedBy = string.Concat(t.User.FirstName, " ", t.User.LastName); });
                    FARInfo.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status;});
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
                FARInfo = FARUtility.GetAllFARs(user.Id).Result;


                if (FARInfo != null && user.ApprovalLevel != 0)
                {
                    FARInforesult = FARInfo.ToList().Where(t => t.RequestId.Equals(search)).ToList();
                    if (FARInforesult != null)
                    {
                        //FARInforesult.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status; t.CreatedBy = string.Concat(t.User.FirstName, " ", t.User.LastName); });
                        FARInforesult.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status;});
                    }
                }
                else if (FARInfo != null && user.ApprovalLevel == 0)
                {
                    FARInforesult = FARInfo.ToList().Where(t => (t.RequestId.Contains(search))).ToList();
                    if (FARInforesult != null)
                    {
                       // FARInforesult.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status; t.CreatedBy = string.Concat(t.User.FirstName, " ", t.User.LastName); });
                        FARInforesult.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status;});
                    }
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