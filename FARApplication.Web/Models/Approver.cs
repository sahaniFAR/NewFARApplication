using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Models
{
    public class Approver
    {
        public int UserId { get; set; }
        public int FarId { get; set; }
        public int Level { get; set; }
        public string ApproverName { get; set; }
        public string  Comments { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
