using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;

namespace Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> AllUsers(bool changes);
        User _user(Guid userID, bool changes);

        void DeleteUser(User user);
        void CreateUser(User user);
    }
}