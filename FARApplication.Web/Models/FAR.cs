using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FARApplication.Web.Models
{
    /// <summary>
    /// This model class maintains the state of a FAR.
    /// </summary>
    public class FAR
    {    {
        public FAR()
        {
            FAREventLogs = new List<FAREventLog>();
            Approverdetails = new List<FARApprover>();
        }
        // Document Information
        public int Id { get; set; }
        public string RequestId { get; set; }
        public int Status { get; set; }
        public DocumentStatus LifeCycleStatus { get; set; }
        public int UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        //Request Information
        [Required(ErrorMessage = "Please enter summary of the FAR.")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "Please enter details of the FAR.")]
        public string Details { get; set; }
        public List<FAREventLog> FAREventLogs { get; set; }
        public User User { get; set; }
        public List<FARApprover> Approverdetails { get; set; }
        [MaxLength(2000, ErrorMessage = "The Filename must be less that 300 characters")]
        public string Filename { get; set; }
    }
}
