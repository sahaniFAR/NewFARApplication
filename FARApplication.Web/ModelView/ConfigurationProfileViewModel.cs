using FARApplication.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;


namespace FARApplication.Web.Models
{
    public class ConfigurationProfileViewModel : ConfigurationProfile
    {

        public List<SelectListItem> ddlApprover1 { get; set; }
        public List<SelectListItem> ddlApprover2 { get; set; }



        public ConfigurationProfileViewModel()
        {
            ddlApprover1 = new List<SelectListItem>();
            ddlApprover2 = new List<SelectListItem>();
        }


    }
}
