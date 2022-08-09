using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web.Models
{
    public class DocuementEventLog
    {
        public int FARId { get; set; }
        public int UserId { get; set; }
        public string  Message { get; set; }
        public DateTime EventDate { get; set; }
    }
}
