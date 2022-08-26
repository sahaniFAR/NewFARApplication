using FARApplication.Data.Data.Entities;
using FARApplication.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FARApplication.Data.Implementation
{
    public class FAREventRepository : IFAREventRepository
    {
        FARContext _context = null;
        public FAREventRepository(FARContext context)
        {
            _context = context;
        }

        public int AddEventDetails(FAREventLog model)
        {
           _context.FAREventLogs.Add(model);
           int result = _context.SaveChanges();
           return result;
        }

        public IEnumerable<FAREventLog> GetEventLogByFARId(int FARId)
        {
            var result = _context.FAREventLogs.ToList().Where(t => t.FAR.Id == FARId);
            return result;
        }

    }
}
