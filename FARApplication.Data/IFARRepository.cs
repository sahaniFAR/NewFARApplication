using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data
{
    public interface IFARRepository
    {
        public IEnumerable<FAR> GetFARById();
        public IEnumerable<FAR> GetFARByStatus();

        public IEnumerable<FAR> GetFARBySubmitter();

        public IEnumerable<FAR> GetFARByNextApprover();
        public int AddFAR(FAR far);
        public string GetFARRequestId();


    }
}
