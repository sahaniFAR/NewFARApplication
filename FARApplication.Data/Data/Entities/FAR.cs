using FARApplication.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Data
{
    public class FAR
    {
        public FAR()
        {
            FAREventLogs = new List<FAREventLog>();
        }
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int Status { get; set; }
       
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }

        public string  Summary { get; set; }

        public string Details { get; set; }
        public List<FAREventLog> FAREventLogs { get; set; }
        public  User  User { get; set; }

        // public IEnumerable<ApproverDetails> ApproverDetails { get; set; }

    }
}
