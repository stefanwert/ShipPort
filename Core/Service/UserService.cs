using Core.Model.Users;
using Core.Repository;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class UserService
    {
        private readonly IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        public Result<User> Create(User user)
        {
            Result<User> ret = UserRepository.Create(user);
            return Result.Success(ret.Value);
        }

        public Maybe<User> DeleteById(Guid id)
        {
            Maybe<User> User = FindById(id);
            if (User.HasNoValue)
                return Maybe.None;
            return UserRepository.DeleteById(id);
        }

        public Maybe<User> FindById(Guid id)
        {
            var user = UserRepository.FindById(id);
            return user == null ? Maybe.None : user;
        }

        public IEnumerable<User> GetAll()
        {
            return UserRepository.GetAll();
        }

        public Result<User> Update(User user)
        {
            Result<User> ret = UserRepository.Update(user);
            return Result.Success(ret.Value);
        }

        public Result<User> DoesUserExists(string email, string password)
        {
            return UserRepository.DoesUserExists(email, password);
        }
    }
}
