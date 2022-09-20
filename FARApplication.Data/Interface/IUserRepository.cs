
using System.Collections.Generic;



namespace FARApplication.Data.Interface
{
    public interface IUserRepository
    {
        bool Create(User user);
        bool Update(User user);
        bool Delete(int Id);
        IEnumerable<User> GetAllUser();
        User GetUserById(int Id);
        User IsValidUser(string email, string password);
        List<User> GetApproverSelectionList();

    }
}
