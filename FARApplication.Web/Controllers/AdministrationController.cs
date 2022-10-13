
using FARApplication.Web.Models;
using FARApplication.Web.ModelView;
using FARApplication.Web.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
         //   bool res = EmailUtility.SendMail().Result;

            ConfigurationProfileViewModel objViewModel = new ConfigurationProfileViewModel();
            ModelState.Clear();
            var Configdata = ConfigurationProfileUtility.GetConfigurationProfileData().Result;

            if (Configdata == null)
            {
                // objLoginViewModel.msg = "Email Id or Password is not correct";
                ViewBag.FailureMessage = "No data Found";
                return View(objViewModel);
            }

            objViewModel.EmailPrincipalName = Configdata.EmailPrincipalName;
            objViewModel.PMOTeamRcvApprovedMail = Configdata.PMOTeamRcvApprovedMail;
            objViewModel.CreatedBy = Configdata.CreatedBy;
            objViewModel.CreatedOn = Configdata.CreatedOn;
            objViewModel.LastModifiedBy = Configdata.LastModifiedBy;
            objViewModel.LastModifiedOn = Configdata.LastModifiedOn;

            // objViewModel = (ConfigurationProfileViewModel)Configdata;

            objViewModel.ddlApprover1 = PopulateApproverDropdown(1); //PopulateApprover1Dropdown(0).Result;   
            objViewModel.Approver1Id = int.Parse(objViewModel.ddlApprover1.Where(a => a.Selected == true).First().Value);
            objViewModel.PrevApprover1Id = objViewModel.Approver1Id;

            objViewModel.ddlApprover2 = PopulateApproverDropdown(2);// PopulateApprover2Dropdown(0).Result;
            objViewModel.Approver2Id = int.Parse(objViewModel.ddlApprover2.Where(a => a.Selected == true).First().Value);
            objViewModel.PrevApprover2Id = objViewModel.Approver2Id;


            TempData["PagePreviousData"] = JsonConvert.SerializeObject(objViewModel);
            return View(objViewModel);
        }
        [HttpPost]
        public IActionResult ConfigurationProfile(ConfigurationProfileViewModel model, string Mode)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.FailureMessage = "There is an issue while saving data!!";

                ConfigurationProfileViewModel objPrevViewmodel = JsonConvert.DeserializeObject<ConfigurationProfileViewModel>(TempData["PagePreviousData"].ToString());

                TempData["PagePreviousData"] = JsonConvert.SerializeObject(objPrevViewmodel);

                return View(objPrevViewmodel);
            }

            if (model.Approver1Id == 0)
            {
                ViewBag.FailureMessage = "Please select Approver 1";

                ConfigurationProfileViewModel objPrevViewmodel = JsonConvert.DeserializeObject<ConfigurationProfileViewModel>(TempData["PagePreviousData"].ToString());
                TempData["PagePreviousData"] = JsonConvert.SerializeObject(objPrevViewmodel);

                return View(objPrevViewmodel);
            }

            if (model.Approver2Id == 0)
            {
                ViewBag.FailureMessage = "Please select Approver 2";

                ConfigurationProfileViewModel objPrevViewmodel = JsonConvert.DeserializeObject<ConfigurationProfileViewModel>(TempData["PagePreviousData"].ToString());
                TempData["PagePreviousData"] = JsonConvert.SerializeObject(objPrevViewmodel);

                return View(objPrevViewmodel);
            }

            if (model.Approver1Id == model.Approver2Id)
            {
                ViewBag.FailureMessage = "Both approver can not be same person";

                ConfigurationProfileViewModel objPrevViewmodel = JsonConvert.DeserializeObject<ConfigurationProfileViewModel>(TempData["PagePreviousData"].ToString());
                TempData["PagePreviousData"] = JsonConvert.SerializeObject(objPrevViewmodel);

                return View(objPrevViewmodel);
            }


            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Mode))
                {

                    string strMessage = string.Empty;
                    string strUserName = string.Empty;

                    var user = HttpContext.Session.getObjectAsJson<User>("UserDetails");
                    if (user != null)
                    {
                        strUserName = string.Concat(user.FirstName, " ", user.LastName);

                    }

                    switch (Mode)
                    {

                        case "Save":
                            strMessage = String.Format("Configuration Profile Modified By {0}", strUserName);
                            break;
                        case "SaveClose":
                            strMessage = String.Format("Configuration Profile Modified By {0}", strUserName);
                            break;

                    }



                    ConfigurationProfile objcf = new ConfigurationProfile();
                    ModelState.Clear();
                    objcf.Approver1Id = model.Approver1Id;
                    objcf.Approver2Id = model.Approver2Id;
                    objcf.EmailPrincipalName = model.EmailPrincipalName;
                    objcf.PMOTeamRcvApprovedMail = model.PMOTeamRcvApprovedMail;
                    objcf.PrevApprover1Id = model.PrevApprover1Id;
                    objcf.PrevApprover1Id = model.PrevApprover2Id;
                    FAREventLog farEventog = FARUtility.PrepareEventLog(strMessage);
                    farEventog.FARId = 0;

                    farEventog.UserId = user.Id;

                    objcf.objlog = farEventog;



                    var result = ConfigurationProfileUtility.UpdateConfigurationProfile(objcf).Result;
                    if (result)
                    {
                        ViewBag.SuccessMessage = "Configuration Profile modified successfully";
                        if (Mode == "SaveClose")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else if (Mode == "Save")
                        {
                            ConfigurationProfileViewModel objCurrentModel = new ConfigurationProfileViewModel();
                            ConfigurationProfileViewModel objPrevViewmodel = JsonConvert.DeserializeObject<ConfigurationProfileViewModel>(TempData["PagePreviousData"].ToString());


                            objCurrentModel = model;
                            objCurrentModel.ddlApprover1 = objPrevViewmodel.ddlApprover1;
                            objCurrentModel.ddlApprover2 = objPrevViewmodel.ddlApprover2;


                            objCurrentModel.LastModifiedBy = strUserName;
                            objCurrentModel.LastModifiedOn = DateTime.Now.ToString("dd-MM-yyyy hh:mm tt");


                            TempData["PagePreviousData"] = JsonConvert.SerializeObject(objCurrentModel);
                            return View(objCurrentModel);
                        }
                        // return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.FailureMessage = "There is some issue to update the record";
                        ConfigurationProfileViewModel objPrevViewmodel = JsonConvert.DeserializeObject<ConfigurationProfileViewModel>(TempData["PagePreviousData"].ToString());
                        TempData["PagePreviousData"] = JsonConvert.SerializeObject(objPrevViewmodel);

                        return View(objPrevViewmodel);
                    }



                }
            }


            // User user = new User() {  }; 

            // ViewBag.SuccessResult = "Record saved succefully!!";


            return View();
        }

        private List<SelectListItem> PopulateApproverDropdown(int Approver1Level)
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
