using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public UserRole Role { get; set; }

        public User()
        {

        }
        public User(Guid id, string email, string password, string name, string surename, UserRole role)
        {
            Id = id;
            Email = email;
            Password = password;
            Name = name;
            Surename = surename;
            Role = role;
        }

        public static Result<User> Create (Guid id, string email, string password, string name, string surename, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<User>("Email is not setted !");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Failure<User>("Password is not setted !");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<User>("Name is not setted !");
            }
            if (string.IsNullOrWhiteSpace(surename))
            {
                return Result.Failure<User>("Surename is not setted !");
            }
            if(role == null)
            {
                return Result.Failure<User>("Role is not setted !");
            }
            Result<User> result = new User(id, email, password, name, surename, role);
            return result;
        }

        public static Result<User> Create(Guid id, string email, string password, string name, string surename, string role)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return Result.Failure<User>("Email is not setted !");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                return Result.Failure<User>("Password is not setted !");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<User>("Name is not setted !");
            }
            if (string.IsNullOrWhiteSpace(surename))
            {
                return Result.Failure<User>("Surename is not setted !");
            }
            if (string.IsNullOrWhiteSpace(role))
            {
                return Result.Failure<User>("Role is not setted !");
            }

            UserRole userRole = Enum.Parse<UserRole>(role);
            if (userRole == null)
            {
                return Result.Failure<User>("Role is not setted !");
            }
            Result<User> result = new User(id, email, password, name, surename, userRole);
            return result;
        }
    }
}
