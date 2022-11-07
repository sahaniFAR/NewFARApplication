using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Please do not leave Email blank.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address!!")]
        public string EmailID { get; set; }
        public string PassWord { get; set; }
        public string msg { get; set; }

    }
}
