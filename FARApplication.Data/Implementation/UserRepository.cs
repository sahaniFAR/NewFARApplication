using FARApplication.Data;
using FARApplication.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FARApplication.Data.Implementation
{
    public class UserRepository : IUserRepository
    {

        private FARContext _context;
        public UserRepository(FARContext context)
        {
            _context = context;
        }
        public bool Create(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public User GetUserById(int Id)
        {

            var rsult = _context.Users;
            var User = _context.Users.Find(Id);

            return User;
        }

        public User IsValidUser(string email, string password)
        {

            var User = _context.Users.FirstOrDefault(u => u.EmailId == email && u.Password == password && u.IsActive == 1);

            return User;
        }

        public List<User> GetApproverSelectionList()
        {
            var User = _context.Users.Where(u => u.IsActive == 1).ToList();
            return User;
        }


        public int Update(ConfigurationProfile objConfigProfile)
        {



            _context.Users.Where(w => w.ApprovalLevel == 1 || w.ApprovalLevel == 2).ToList().ForEach(i => i.ApprovalLevel = 0);

            _context.Users.Where(w => w.Id == objConfigProfile.Approver1Id).ToList().ForEach(i => i.ApprovalLevel = 1);
            _context.Users.Where(w => w.Id == objConfigProfile.Approver2Id).ToList().ForEach(i => i.ApprovalLevel = 2);

            _context.FAREventLogs.Add(objConfigProfile.objlog);


            return _context.SaveChanges();
        }

        public bool UpdatePassword(string emailId, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.EmailId == emailId);
            _context.Entry(user).Property(p => p.Password).CurrentValue = password;
            var result = _context.SaveChanges();

            return result > 0? true: false;

        }

        public User GetUserByRole(int approvalLevel)
        {
            var user = _context.Users.FirstOrDefault(t => t.ApprovalLevel == approvalLevel);
            return user;
        }

        public string GetUserEmailIdsOnRole(int ApprovalLevel)
        {
            List<string> strEmailIds = _context.Users.Where(t => t.ApprovalLevel == ApprovalLevel).Select(t => t.EmailId).ToList();
            return string.Join(',', strEmailIds);
        }
    }
}
