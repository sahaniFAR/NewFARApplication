using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Models
{
    public class User
    {
        /// <summary>
        /// This model class maintains the state of an User.
        /// </summary>
        public int Id { get; set; }

        public string  FirstName { get; set; }

        public string LastName { get; set; }

        public string  EmailId { get; set; }

        public string Password { get; set; }
       
        public Level  ApprovalLevel { get; set; }
        public int IsActive { get; set; }
    }
}
