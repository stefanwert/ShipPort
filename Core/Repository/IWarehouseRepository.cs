using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Core.Model;

namespace Core.Repository
{
    public interface IWarehouseRepository
    {
        Maybe<Warehouse> FindById(Guid id);
        IEnumerable<Warehouse> GetAll();
        IEnumerable<Warehouse> GetAllWithCargos();
        Result<Warehouse> Create(Warehouse warehouse);
        Maybe<Warehouse> DeleteById(Guid id);
        Result<Warehouse> Update(Warehouse warehouse);
        ICollection<Warehouse> FindByShipPortId(Guid id);
    }
}
