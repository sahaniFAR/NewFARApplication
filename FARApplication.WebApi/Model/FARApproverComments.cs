using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.WebApi.Model
{
    public class FARApproverComments
    {
        public int Id { get; set; }
        public int FARId { get; set; }

        public int ApproverId { get; set; }
        public string Comments { get; set; }

        public string Signature { get; set; }

        public DateTime SignatureDateTime { get; set; }
    }
}
