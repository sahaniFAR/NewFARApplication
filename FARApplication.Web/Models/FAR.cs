using System;


namespace FARApplication.Web.Models
{
    /// <summary>
    /// This model class maintains the state of a FAR.
    /// </summary>
    public class FAR
    {
        // Document Information
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        //Request Information
        public string Summary { get; set; }
        public string Details { get; set; }
    }
}
