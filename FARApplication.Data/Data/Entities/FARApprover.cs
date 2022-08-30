using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data.Data.Entities
{
    public class FARApprover
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FARId { get; set; }
        public string Comments { get; set; }
        public DateTime ApprovedDate { get; set; }
        public FAR FAR { get; set; }


    }
}
