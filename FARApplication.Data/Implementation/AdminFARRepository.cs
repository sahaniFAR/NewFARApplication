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

        public IEnumerable<FAR> GetAllFAR()
        {
            throw new NotImplementedException();
        }

        public FARCustom GetAllFAROnSubmiter(int submiterId, int pageSize, int currentPageIndx)
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

        int IFARRepository.AddFAR(FAR far)
        {
            throw new NotImplementedException();
        }

        IEnumerable<FAR> IFARRepository.GetAllFAR()
        {
            throw new NotImplementedException();
        }

        FARCustom IFARRepository.GetallFARBasedOnStatus(int status, int pageCount, int pageIndex)
        {
            throw new NotImplementedException();
        }

        FARCustom IFARRepository.GetallFARBasedOnStatusOnUser(int userId, int status, int PageCount, int pageIndex)
        {
            throw new NotImplementedException();
        }

        FARCustom IFARRepository.GetAllPagedFAR(int pageCount, int pageIndex)
        {
            throw new NotImplementedException();
        }

        

        IEnumerable<FAR> IFARRepository.GetFARById(int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<FAR> IFARRepository.GetFARByNextApprover()
        {
            throw new NotImplementedException();
        }

        IEnumerable<FAR> IFARRepository.GetFARByStatus(int userId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<FAR> IFARRepository.GetFARBySubmitter()
        {
            throw new NotImplementedException();
        }

        IEnumerable<FAR> IFARRepository.GetFARByUserId(int userId, int pageCount, int pageIndex)
        {
            throw new NotImplementedException();
        }

        FAR IFARRepository.getFARDetails(int FarId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<FAR> IFARRepository.getFARListForApprovers(int status)
        {
            throw new NotImplementedException();
        }

        string IFARRepository.GetFARRequestId()
        {
            throw new NotImplementedException();
        }

        int IFARRepository.Update(FAR far)
        {
            throw new NotImplementedException();
        }
    }
}
