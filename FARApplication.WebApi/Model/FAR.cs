using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.WebApi.Model
{
    public class FAR
    {
        public int Id { get;}
        public string RequestId { get; set; }
        public DocumentStatus Status { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string  Summary { get; set; }

        public string Details { get; set; }

        public IEnumerable<ApproverDetails> ApproverDetails { get; set; }

    }
}
