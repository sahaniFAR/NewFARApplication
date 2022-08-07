using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARApplication.WebApi.Model
{
    public enum DocumentStatus
    {
        Draft = 1,
        AwaitingApproval = 2,
        Approved = 3, 
        Rejected = 4

    }
}
