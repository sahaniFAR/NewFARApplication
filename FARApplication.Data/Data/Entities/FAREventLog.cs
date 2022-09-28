using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data.Data.Entities
{
    public class FAREventLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime EventDate { get; set; }
        public int FARId { get; set; }

        public int? UserId { get; set; }
        public FAR FAR { get; set; }
        public User User { get; set; }
    }
}
