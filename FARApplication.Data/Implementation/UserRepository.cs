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

            var User = _context.Users.FirstOrDefault(u => u.EmailId == email && u.Password == password && u.IsActive==1);
               
            return User;
        }

        public List<User> GetApproverSelectionList()
        {
            var User = _context.Users.Where(u => u.IsActive == 1).ToList();
            return User;
        }


        public bool Update(User user)
            {
                throw new NotImplementedException();
            }
        

       
    }
}
