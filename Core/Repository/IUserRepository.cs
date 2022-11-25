using Core.Model.Users;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IUserRepository
    {
        Maybe<User> FindById(Guid id);
        IEnumerable<User> GetAll();
        Result<User> Create(User user);
        Maybe<User> DeleteById(Guid id);
        Result<User> Update(User user);
        Result<User> DoesUserExists(string email, string password);
    }
}
