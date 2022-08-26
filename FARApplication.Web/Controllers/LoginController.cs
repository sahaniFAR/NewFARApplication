using FARApplication.Web.Models;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            LoginViewModel objLoginViewModel = new LoginViewModel();
            objLoginViewModel.EmailID = "";
            objLoginViewModel.PassWord = "";
            objLoginViewModel.msg = "";
            return View(objLoginViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            LoginViewModel objLoginViewModel = new LoginViewModel();
            objLoginViewModel.EmailID = "";
            objLoginViewModel.PassWord = "";
            objLoginViewModel.msg = "";
            var validUser  = UserUtility.IsValidUser(model.EmailID, model.PassWord).Result;

            if (validUser == null)
            {
                objLoginViewModel.msg = "Email Id or Password is not correct";
                ViewData["MSGErr"] = "Email Id or Password is not correct";
            }

            else
            {
                
               // ViewData["MSGSuc"] = "Email Id or Password is Validated";

               HttpContext.Session.SetString("UserEmail", model.EmailID);
               HttpContext.Session.setObjectAsJson("UserDetails", validUser);

                return RedirectToAction("Index", "Home");
                
            }

            return View(objLoginViewModel);
        }


    }
}
