using System;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;

namespace Core.Model.Workers
{
    public class WarehouseClerk : Worker
    {
        [Required]
        public ClerkRole ClerkRole { get; private set; }

        public Guid WarehouseId { get; private set; }

        private WarehouseClerk() { }
        private WarehouseClerk(ClerkRole clerkRole, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable, Guid shipPortId, Guid warehouseId)
            : base(id, name, surname, age, yearsOfWorking, salary, isAvailable, shipPortId)
        {
            ClerkRole = clerkRole;
            WarehouseId = warehouseId;
        }

        public static Result<WarehouseClerk> Create(ClerkRole clerkRole, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable, Guid shipPortId, Guid warehouseId)
        {
            if (clerkRole == null)
            {
                return Result.Failure<WarehouseClerk>("Clerk role is not setted !");
            }
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<WarehouseClerk>("Name is not setted !");
            }
            if (string.IsNullOrEmpty(surname))
            {
                return Result.Failure<WarehouseClerk>("Surname is not setted !");
            }
            if (age == null || age < 0)
            {
                return Result.Failure<WarehouseClerk>("Age is not setted or it is negative!");
            }
            if (yearsOfWorking == null || yearsOfWorking < 0)
            {
                return Result.Failure<WarehouseClerk>("Years of working is not setted or it is negative!");
            }
            if (salary == null || salary < 0)
            {
                return Result.Failure<WarehouseClerk>("Salary is not setted or it is negative!");
            }
            if (isAvailable == null)
            {
                return Result.Failure<WarehouseClerk>("Is available is not setted or it is negative!");
            }
            if(warehouseId == Guid.Empty)
            {
                return Result.Failure<WarehouseClerk>("Warehouse id is note setted for crew member");
            }
            Result<WarehouseClerk> result = new WarehouseClerk(clerkRole, id, name, surname, age, yearsOfWorking, salary, isAvailable, shipPortId, warehouseId);
            return result;
        }
    }
}
