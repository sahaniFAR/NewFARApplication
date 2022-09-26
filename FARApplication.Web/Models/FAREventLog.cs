using System;


namespace FARApplication.Web.Models
{

    /// <summary>
    /// This model class maintains the state of the DocumentEventLog per FAR.
    /// </summary>
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
