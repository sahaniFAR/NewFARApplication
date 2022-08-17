using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            return _context.FARs.ToList().OrderBy(t => t.Id);
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
    }
}
