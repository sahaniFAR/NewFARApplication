using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Models
{
    /// <summary>
    /// This model class maintains the state of Approver per FAR.
    /// </summary>
    public class Approver
    {
        public int Id { get; set; }
        public int  FARId { get; set; }
        public int UserId { get; set; }
        public string Comments { get; set; }
        public DateTime ApprovalDate { get; set; }

    }
}
