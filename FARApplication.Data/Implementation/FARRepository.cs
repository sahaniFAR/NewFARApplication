﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FARApplication.Data.Interface;


namespace FARApplication.Data.Implementation
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

        public IEnumerable<FAR> GetAllFAR()
        { 
            // Show the latest records first
            return _context.FARs.Include(t => t.User).OrderByDescending(t=> t.CreatedOn).ThenByDescending(t => t.RequestId).ToList();
        }

        public IEnumerable<FAR> GetFARById(int userId)
        {
           // return _context.FARs.ToList().OrderBy(t => t.Id);
           return  _context.FARs.Include(t => t.User).Where(t=> t.UserId == userId).ToList().OrderByDescending(u => u.CreatedOn).ThenByDescending(u => u.RequestId);
        }

        public IEnumerable<FAR> GetFARByNextApprover()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FAR> GetFARByStatus(int userId)
        {
            return _context.FARs.Where(t => t.UserId == userId).ToList().OrderBy(t => t.Status);
        }

        public IEnumerable<FAR> GetFARBySubmitter()
        {
            throw new NotImplementedException();
        }

        public FAR getFARDetails(int FarId)
        {

            try
            {
                var result = _context.FARs.Include(n => n.FAREventLogs).Include(t => t.User).Include(u => u.Approverdetails).FirstOrDefault(n => n.Id == FarId);
                return result;
            }
            catch (Exception ex) 
            { 
                var exception =  ex;
                throw ex;
            }
            
            
        }

        public IEnumerable<FAR> getFARListForApprovers(int status)
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

        public int Update(FAR far)
        {
          //  _context.Attach(far);
          //  _context.Entry(far).State = EntityState.Modified;
            var originalfar = _context.FARs
                             .Include(t => t.FAREventLogs)
                             .Include(t => t.Approverdetails).Single(t => t.Id == far.Id);
            _context.Entry(originalfar).CurrentValues.SetValues(far);

            if (far.Approverdetails !=null)
            {
                if (originalfar.Approverdetails != null)
                {
                    var indexFARApprover = originalfar.Approverdetails.Count;


                    for (int counter = 0; counter < far.Approverdetails.Count; counter++)
                    {
                        if (indexFARApprover == far.Approverdetails.Count)
                            _context.Entry(originalfar.Approverdetails[counter]).CurrentValues.SetValues(far.Approverdetails[counter]);
                        else
                        {
                            var farApprover = far.Approverdetails[indexFARApprover];
                            _context.Attach(farApprover);
                            _context.Entry(farApprover).State = EntityState.Added;
                        }

                    }
                }
            }

            if (far.FAREventLogs != null)
            {
                int index = originalfar.FAREventLogs.Count;
                var farEventLog = far.FAREventLogs[index];
                _context.Attach(farEventLog);
                _context.Entry(farEventLog).State = EntityState.Added;
            }

            //_context.Entry(originalfar.Approverdetails).CurrentValues.SetValues(far.Approverdetails);
            //_context.Entry(originalfar.FAREventLogs).CurrentValues.SetValues(far.FAREventLogs);
            // _context.Entry(originalfar.Approverdetails).CurrentValues.SetValues(far.Approverdetails);

            return _context.SaveChanges();
        }
    }
}
