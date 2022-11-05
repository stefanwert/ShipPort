using Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface ICargoRepository
    {
        Maybe<Cargo> FindById(Guid id);
        IEnumerable<Cargo> GetAll();
        IEnumerable<Cargo> GetAllThatIsNotTrasnporting(Guid warehouseId);

        IEnumerable<Cargo> GetAllFromWarehouse(Guid warehouseId);
        Result<Cargo> Create(Cargo cargo);
        Maybe<Cargo> DeleteById(Guid id);
        Result<Cargo> Update(Cargo cargo);
    }
}
