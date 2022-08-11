using System;


namespace FARApplication.Data
{
    public class FAR
    {
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int Status { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string  Summary { get; set; }

        public string Details { get; set; }

       // public IEnumerable<ApproverDetails> ApproverDetails { get; set; }

    }
}
