//using FARApplication.Service.Controllers;
using FARApplication.Web.Models;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

            FARViewModel FARViewModel = new FARViewModel();
            var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");

            if (user != null)
            {

                ViewBag.SessionUser = user;
                var userId = user.Id;

                if (user.ApprovalLevel == 0)
                {
                    ViewBag.Mode = "User";
                    FARViewModel = SearchUtility.GetAllFARBasedOnSubmiter(userId, 1).Result;
                }
                else if ((int)user.ApprovalLevel == 3)
                {
                    ViewBag.Mode = "Reader";
                    FARViewModel = FARUtility.GetAllFAROnStatus(4, 1).Result;
                }
                else
                {
                    ViewBag.Mode = "Admin";
                    FARViewModel = FARUtility.GetALLPAGEDFAR(1).Result;
                   
                }

                if (FARViewModel.FARs != null && FARViewModel.FARs.Count() > 0)
                {

                    FARViewModel.FARs.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status;});
                    FARViewModel.CurrentPageIndex = 1; // Hardcoded value as current page index alwyas will be 1 before loading.
                    double pageCount = (double)(Convert.ToDecimal(FARViewModel.TotalRecordCount) / Convert.ToDecimal(10));
                    FARViewModel.PageCount = (int)Math.Ceiling(pageCount);
                   
                }
                else
                {
                    FARViewModel.FARs = new List<FAR>();

                }
            }

            return View(FARViewModel);
        }
        [HttpPost]
        public ActionResult Index(string SelectedStatus, string search, int currentPageIndex = 1)
        {

            FARViewModel FARViewModel = new FARViewModel();
            var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
            if (currentPageIndex == 0)
                currentPageIndex = 1;

            if (user != null)
            {
                ViewBag.SessionUser = user;
                if (user.ApprovalLevel == 0)
                {
                    ViewBag.Mode = "User";
                }
                else if ((int)user.ApprovalLevel == 3)
                {
                    ViewBag.Mode = "Reader";
                }
                else
                {
                    ViewBag.Mode = "Admin";

                }
                var userId = user.Id;

                if(string.IsNullOrEmpty(SelectedStatus))
                {
                    if (user.ApprovalLevel == 0)
                    {
                      
                        FARViewModel = SearchUtility.GetAllFARBasedOnSubmiter(userId, currentPageIndex).Result;
                    }
                    else if((int)user.ApprovalLevel == 3)
                    {
                        int statusId = (int)Convert.ToInt32(SelectedStatus);
                        FARViewModel = FARUtility.GetAllFAROnStatus(statusId, currentPageIndex).Result;
                    }
                    else
                    {
                        
                        FARViewModel = FARUtility.GetALLPAGEDFAR(currentPageIndex).Result;

                    }

                }

                else
                {
                    int statusId = (int)Convert.ToInt32(SelectedStatus);
                    FARViewModel = SearchUtility.GetAllFAROnStatus(statusId, currentPageIndex).Result;
                }

                // If the user seraches with request id
               
                if(!string.IsNullOrEmpty(search))
                {
                    var result = FARViewModel.FARs.Where(t => t.RequestId.Contains(search)).ToList();
                    if (result.Count() > 0)
                    {
                        FARViewModel.FARs.Clear();
                        FARViewModel.FARs.AddRange(result);
                        FARViewModel.TotalRecordCount = result.Count();
                    }
                }

                if (FARViewModel.FARs != null )
                {
                    FARViewModel.FARs.ForEach(t => { t.LifeCycleStatus = (DocumentStatus)t.Status; });
                    FARViewModel.CurrentPageIndex = currentPageIndex;
                    double pageCount = (double)(Convert.ToDecimal(FARViewModel.TotalRecordCount) / Convert.ToDecimal(10));
                    FARViewModel.PageCount = (int)Math.Ceiling(pageCount);


                }
                else
                {
                    FARViewModel.FARs = new List<FAR>();

                }
            }

            return View("Index", FARViewModel);
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