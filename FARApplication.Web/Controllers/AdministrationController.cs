
using FARApplication.Web.Models;
using FARApplication.Web.ModelView;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace FARApplication.Web.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult ConfigurationProfile()
        {
            ConfigurationProfileViewModel objViewModel = new ConfigurationProfileViewModel();
         
            var Configdata = ConfigurationProfileUtility.GetConfigurationProfileData().Result;

            if (Configdata == null)
            {
               // objLoginViewModel.msg = "Email Id or Password is not correct";
                ViewData["MSGErr"] = "No data Found";
                return View();
            }
            objViewModel.EmailPrincipalName = Configdata.EmailPrincipalName;
            objViewModel.PMOTeamRcvApprovedMail = Configdata.PMOTeamRcvApprovedMail;

            return View(objViewModel);
        }
    }

   
}
