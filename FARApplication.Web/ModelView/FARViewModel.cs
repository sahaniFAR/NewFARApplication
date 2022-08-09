using FARApplication.Web.ModelView;
using System;
using System.Collections.Generic;


namespace FARApplication.Web.Models
{
    public class FARViewModel
    {
        // Document Information
        public int Id { get; set; }
        public string RequestId { get; set; }
        public DocumentStatus Status { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        //Request Information
        public string Summary { get; set; }
        public string Details { get; set; }

        //Approver Information
        public ApproverViewModel Approvers { get; set; }

        //Document Evecnt Log
         List<FAREventLog> FAREventLogs { get; set; }


    }
}
