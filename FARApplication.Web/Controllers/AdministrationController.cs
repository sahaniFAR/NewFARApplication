
using FARApplication.Web.Models;
using FARApplication.Web.ModelView;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


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
                ViewBag.FailureMessage = "No data Found";
                return View(objViewModel);
            }

            objViewModel.EmailPrincipalName = Configdata.EmailPrincipalName;
            objViewModel.PMOTeamRcvApprovedMail = Configdata.PMOTeamRcvApprovedMail;

            objViewModel.ddlApprover1 = PopulateApproverDropdown(1); //PopulateApprover1Dropdown(0).Result;   
            objViewModel.Approver1Id = int.Parse(objViewModel.ddlApprover1.Where(a => a.Selected == true).First().Value);
           

            objViewModel.ddlApprover2 = PopulateApproverDropdown(2);// PopulateApprover2Dropdown(0).Result;
            objViewModel.Approver2Id = int.Parse(objViewModel.ddlApprover2.Where(a => a.Selected == true).First().Value);


            return View(objViewModel);
        }
        [HttpPost]
        public IActionResult ConfigurationProfile(ConfigurationProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.FailureMessage = "There is an issue while saving data!!";
                return View(model);
            }

           // User user = new User() {  }; 

            ViewBag.SuccessResult = "Record saved succefully!!";
            return View();
        }

          private  List<SelectListItem> PopulateApproverDropdown(int Approver1Level)
        {
            ConfigurationProfileViewModel objviewmodel = new ConfigurationProfileViewModel();
            var UserList = UserUtility.GetApproverSelectionList().Result;
            var UserSelectedItemList = UserList.Select(t => new SelectListItem()
            {
                Value = t.Id.ToString(),
                Text = String.Concat(t.FirstName, "", t.LastName),
                Selected = (int)t.ApprovalLevel == Approver1Level 
            }).ToList();

            UserSelectedItemList.Insert(0, new SelectListItem() { Value = "0", Text = "--Select Approver--" });
           
            return UserSelectedItemList;

        }

      
    }
   
}
