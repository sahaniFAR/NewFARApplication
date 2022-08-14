using FARApplication.Web.Models;
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
    }
}
