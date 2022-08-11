using FARApplication.Data;
using FARApplication.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

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
            var User = _context.Users.Find(Id);
            return User;
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
