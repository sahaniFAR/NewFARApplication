using FARApplication.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FARApplication.Data.Implementation
{
    public class AdminFARRepository : IFARRepository
    {
        private FARContext _context;
        public AdminFARRepository(FARContext context)
        {
            _context = context;

        }
        public int AddFAR(FAR far)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FAR> GetFARById(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FAR> GetFARByNextApprover()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FAR> GetFARByStatus(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FAR> GetFARBySubmitter()
        {
            throw new NotImplementedException();
        }

        public FAR getFARDetails(int FarId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FAR> getFARListForApprovers(int status)
        {
            var result = _context.FARs.Where(t => t.Status == status).ToList();
            return result;
        }

        public string GetFARRequestId()
        {
            throw new NotImplementedException();
        }

        public int Update(FAR far)
        {

            _context.FARs.Update(far);
            return _context.SaveChanges();
            
        }
    }
}
