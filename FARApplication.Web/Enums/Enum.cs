using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.Web
{
    public enum DocumentStatus
    {
        Draft = 1,
        SendForApproval = 2,
        ApprovedByFirstLevel = 3,
        Approved = 4,
        Rejected = 5
    }

    public enum Level
    {
        None= 0,
        FirstLevel = 1,
        SecondLevel = 2
    }
}
