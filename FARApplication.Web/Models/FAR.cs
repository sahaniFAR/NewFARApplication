using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Models
{
    public class FAR
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Summary { get; set; }

        public string Details { get; set; }

    }
}
