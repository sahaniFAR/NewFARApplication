//using FARApplication.Service.Controllers;
using FARApplication.Web.Models;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Diagnostics;


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
                FARInfo = FARUtility.GetAllFARs(userId).Result;

                if (FARInfo != null)
                {
                    FARInfo.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status; t.CreatedBy = string.Concat(t.User.FirstName, " ", t.User.LastName); });
                }
            }

            return View(FARInfo);
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