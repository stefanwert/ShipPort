using System;
using CSharpFunctionalExtensions;

namespace Entities.Model.Workers
{
    public class WarehouseClerk : Worker
    {
        public ClerkRole ClerkRole { get; private set; }

        private WarehouseClerk() { }
        private WarehouseClerk(ClerkRole clerkRole, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable)
            : base(id, name, surname, age, yearsOfWorking, salary, isAvailable)
        {
            ClerkRole = clerkRole;
        }

        public static Result<WarehouseClerk> Create(ClerkRole clerkRole, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable)
        {
            if (clerkRole == null)
            {
                Result.Failure<WarehouseClerk>("Clerk role is not setted !");
            }
            if (string.IsNullOrEmpty(name))
            {
                Result.Failure<WarehouseClerk>("Name is not setted !");
            }
            if (string.IsNullOrEmpty(surname))
            {
                Result.Failure<WarehouseClerk>("Surname is not setted !");
            }
            if (age == null || age < 0)
            {
                Result.Failure<WarehouseClerk>("Age is not setted or it is negative!");
            }
            if (yearsOfWorking == null || yearsOfWorking < 0)
            {
                Result.Failure<WarehouseClerk>("Years of working is not setted or it is negative!");
            }
            if (salary == null || salary < 0)
            {
                Result.Failure<WarehouseClerk>("Salary is not setted or it is negative!");
            }
            if (isAvailable == null)
            {
                Result.Failure<WarehouseClerk>("Is available is not setted or it is negative!");
            }
            Result<WarehouseClerk> result = new WarehouseClerk(clerkRole, id, name, surname, age, yearsOfWorking, salary, isAvailable);
            return result;
        }
    }
}
