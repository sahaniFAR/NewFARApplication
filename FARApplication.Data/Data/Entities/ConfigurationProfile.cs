using FARApplication.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data
{
    public class ConfigurationProfile
    {
        /// </summary>
        /// 
        public int PrevApprover1Id { get; set; }
        public int PrevApprover2Id { get; set; }

        public int Approver1Id { get; set; }

        public int Approver2Id { get; set; }
        public string EmailPrincipalName { get; set; }

        public string PMOTeamRcvApprovedMail { get; set; }
        public FAREventLog objlog { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedOn { get; set; }

        public string LastModifiedBy { get; set; }
        public string LastModifiedOn { get; set; }



        public ConfigurationProfile()
        {

            objlog = new FAREventLog();

        }

    }
}
