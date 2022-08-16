using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data
{
   public  class User
    {
        /// </summary>
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailId { get; set; }

        public string Password { get; set; }

        public int ApprovalLevel { get; set; }
        public int IsActive { get; set; }
    }
}
