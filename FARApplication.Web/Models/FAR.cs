using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Models
{
    public class FAR
    {
        public int Id { get; set; }
        // Document Information
        public string RequestId { get; set; }
        public int Status { get; set; } // 1 = Draft , 2 = Send for Approval, 3 = First Level approved, 4 = approved, 5=Rejected
        public int UserId { get; set; } // Created By. No user Input, User Id should be saved based on the login email
        public DateTime CreatationDate { get; set; } // Sytem Date Time. No Input. 

        // Request Information

        public string Summary { get; set; }
        public string Details { get; set; }

        // Approver Information
        public List<Approver> Approvers { get; set; }

        //Document Event Log
        public List<DocuementEventLog> DocuementEventLogs { get; set; }

    }
}
