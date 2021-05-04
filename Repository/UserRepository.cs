using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<User> AllUsers(bool changes) =>
            FindAll(changes).OrderBy(x => x.UserName).ToList();

        public IEnumerable<User> GetByID(IEnumerable<Guid> Ids, bool changes) =>
            FindByCondition(x => Ids.Contains(x.Id), changes).ToList();

        public User _user(Guid userID, bool changes) =>
            FindByCondition(x => x.Id.Equals(userID), changes).SingleOrDefault();

        public void DeleteUser(User user)
        { 
            Delete(user);
        }


        public void CreateUser(User user) =>
            Create(user);


    }
}