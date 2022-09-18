using FARApplication.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;


namespace FARApplication.Web.Models
{
    public class ConfigurationProfileViewModel
    {
        public string EmailPrincipalName { get; set; }

        public string PMOTeamRcvApprovedMail { get; set; }
        public List<SelectListItem> ddlApprover1 { get; set; }
        public List<SelectListItem> ddlApprover2 { get; set; }

        public int Approver1Id { get; set; }
        public int Approver2Id { get; set; }

        public ConfigurationProfileViewModel()
        {
            ddlApprover1 = new List<SelectListItem>();
            ddlApprover2 = new List<SelectListItem>();
        }


    }
}
