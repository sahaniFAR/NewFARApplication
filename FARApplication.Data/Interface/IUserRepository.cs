﻿using FARApplication.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FARApplication.Data.Interface
{
    public interface IUserRepository
    {
        bool Create(User user);
        int Update(ConfigurationProfile objConfigProfile);
        bool Delete(int Id);
        IEnumerable<User> GetAllUser();
        User GetUserById(int Id);
        User IsValidUser(string email, string password);
        List<User> GetApproverSelectionList();

    }
}
