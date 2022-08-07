using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.WebApi.Model
{
    public class ApproverDetails
    {
        public int Id { get; set; }

        public string ApproverName { get; set; }

        public string ApproverIBMEmail { get; set; }

        public int  Status { get; set; }
    }
}
