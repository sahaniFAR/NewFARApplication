using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data.Interface
{
    public interface IFARRepository
    {
        public IEnumerable<FAR> GetAllFAR();
        public FARCustom GetAllPagedFAR(int pageCount, int pageIndex);
        public IEnumerable<FAR> GetFARById(int userId);
        public IEnumerable<FAR> GetFARByUserId(int userId, int pageCount, int pageIndex);
        public IEnumerable<FAR> GetFARByStatus(int userId);

        public IEnumerable<FAR> GetFARBySubmitter();

        public IEnumerable<FAR> GetFARByNextApprover();
        public int AddFAR(FAR far);
        public string GetFARRequestId();
        public FAR getFARDetails(int FarId);
        public IEnumerable<FAR> getFARListForApprovers(int status);
        public int Update(FAR far);
        public FARCustom GetallFARBasedOnStatus(int status, int pageCount, int pageIndex);
        public FARCustom GetallFARBasedOnStatusOnUser(int userId, int status, int pageCount, int pageIndex);
        public FARCustom GetAllFAROnSubmiter(int submiterId, int pageSize, int currentPageIndx);


    }
}
