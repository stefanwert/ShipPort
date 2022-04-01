using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Core.Model.Workers;

namespace Core.Repository
{
    public interface IWarehouseClerkRepository
    {
        Maybe<WarehouseClerk> FindById(Guid id);
        IEnumerable<WarehouseClerk> GetAll();
        Result<WarehouseClerk> Create(WarehouseClerk warehouseClerk);
        Maybe<WarehouseClerk> DeleteById(Guid id);
        Result<WarehouseClerk> Update(WarehouseClerk warehouseClerk);
    }
}
