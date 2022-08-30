using System;


namespace FARApplication.Web.Models
{
    /// <summary>
    /// This model class maintains the state of Approver per FAR.
    /// </summary>
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
