using Core.Model.Users;
using Core.Repository;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly Database Database;

        public UserRepository(Database database)
        {
            Database = database;
        }
        public Result<User> Create(User user)
        {
            Result<User> ret = Database.Users.Add(user).Entity;
            Database.SaveChanges();
            return ret;
        }

        public Maybe<User> DeleteById(Guid id)
        {
            var user = Database.Users.First(x => x.Id == id);
            Database.Users.Remove(user);
            Database.SaveChanges();
            return user;
        }

        public Result<User> DoesUserExists(string email, string password)
        {
            Result<User> ret = Database.Users.Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();
            return ret;
        }   

        public Maybe<User> FindById(Guid id)
        {
            var user = Database.Users.Find(id);
            return user == null ? Maybe.None : user;
        }

        public IEnumerable<User> GetAll()
        {
            return Database.Users;
        }

        public Result<User> Update(User user)
        {
            Result<User> result = Database.Users.Update(user).Entity;
            Database.SaveChanges();
            return result;
        }
    }
}
