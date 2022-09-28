using FARApplication.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data.Interface
{
    public interface IFAREventRepository
    {
        IEnumerable<FAREventLog> GetEventLogByFARId(int FARId);
        IEnumerable<FAREventLog> GetEventLogByFAR(int FARId);
        int AddEventDetails(FAREventLog model);
    }
};
