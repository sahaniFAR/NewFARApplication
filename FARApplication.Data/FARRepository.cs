using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FARApplication.Data
{
    public class FARRepository : IFARRepository
    {
        private FARContext _context;
        public FARRepository(FARContext context)
        {
            _context = context;
        }

        public int AddFAR(FAR far)
        {
            _context.FARs.Add(far);
             return _context.SaveChanges();
           
        }

        public IEnumerable<FAR> GetFARById()
        {
           // return _context.FARs.ToList().OrderBy(t => t.Id);
           return  _context.FARs.Include(t => t.User).ToList().OrderBy(u => u.Id);
        }

        public IEnumerable<FAR> GetFARByNextApprover()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FAR> GetFARByStatus()
        {
            return _context.FARs.ToList().OrderBy(t => t.Status);
        }

        public IEnumerable<FAR> GetFARBySubmitter()
        {
            throw new NotImplementedException();
        }
        public string GetFARRequestId()
        {
            var result = _context.FARs.FromSqlRaw("[dbo].[Sp_GetLastFARRecord]").ToList();
            string farRequestId = string.Empty;
            if (result.Count > 0)
            {
                var lastrequestId = result[0].RequestId;
                var arrRequestId = lastrequestId.Split('-');
                farRequestId = arrRequestId[3].ToString();

            }

            return farRequestId;
        }
    }
}
