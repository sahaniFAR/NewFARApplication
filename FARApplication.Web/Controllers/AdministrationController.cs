
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
                ViewData["MSGErr"] = "No data Found";
                return View();
            }
            objViewModel.EmailPrincipalName = Configdata.EmailPrincipalName;
            objViewModel.PMOTeamRcvApprovedMail = Configdata.PMOTeamRcvApprovedMail;



            objViewModel.ddlApprover1 = PopulateApprover1Dropdown(0).Result;   
            objViewModel.Approver1Id = int.Parse(objViewModel.ddlApprover1.Where(a => a.Selected == true).First().Value);


            objViewModel.ddlApprover2 = PopulateApprover2Dropdown(0).Result;
            objViewModel.Approver2Id = int.Parse(objViewModel.ddlApprover2.Where(a => a.Selected == true).First().Value);


            return View(objViewModel);
        }


        public static async Task<List<SelectListItem>> PopulateApprover1Dropdown(int Approver1id)
        {
            ConfigurationProfileViewModel objviewmodel = new ConfigurationProfileViewModel(); 
            var ApproverList = UserUtility.GetApproverSelectionList().Result;
            int countApprover1 = 0;
            foreach (var i in ApproverList)
            {
                if ((int)i.ApprovalLevel == 1)
                {
                    objviewmodel.ddlApprover1.Add(new SelectListItem { Value = Convert.ToString(i.Id), Text = i.FirstName + " " + i.LastName, Selected = true });
                    countApprover1++;
                }
                else
                {
                    objviewmodel.ddlApprover1.Add(new SelectListItem { Value = Convert.ToString(i.Id), Text = i.FirstName + " " + i.LastName });
                }
            }
            if(countApprover1==0)
            { 
            objviewmodel.ddlApprover1.Insert(0, (new SelectListItem { Value = "0", Text = "--Select Approver--", Selected = true }));
            }
            else
            {
                objviewmodel.ddlApprover1.Insert(0, (new SelectListItem { Value = "0", Text = "--Select Approver--" }));
            }
            return objviewmodel.ddlApprover1;

        }

        public static async Task<List<SelectListItem>> PopulateApprover2Dropdown(int Approver1id)
        {
            ConfigurationProfileViewModel objviewmodel = new ConfigurationProfileViewModel();
            var ApproverList = UserUtility.GetApproverSelectionList().Result;
            int countApprover2 = 0;
            foreach (var i in ApproverList)
            {
                if ((int)i.ApprovalLevel == 2)
                {
                    objviewmodel.ddlApprover2.Add(new SelectListItem { Value = Convert.ToString(i.Id), Text = i.FirstName + " " + i.LastName, Selected = true });
                    countApprover2++;
                }
                else
                {
                    objviewmodel.ddlApprover2.Add(new SelectListItem { Value = Convert.ToString(i.Id), Text = i.FirstName + " " + i.LastName });
                }
            }
            if (countApprover2 == 0)
            {
                objviewmodel.ddlApprover2.Insert(0, (new SelectListItem { Value = "0", Text = "--Select Approver--", Selected = true }));
            }
            else
            {
                objviewmodel.ddlApprover2.Insert(0, (new SelectListItem { Value = "0", Text = "--Select Approver--" }));
            }
            return objviewmodel.ddlApprover2;

        }

    }
   
}
