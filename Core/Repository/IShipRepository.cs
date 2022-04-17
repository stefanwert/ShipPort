using Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IShipRepository
    {
        Maybe<Ship> FindById(Guid id);
        IEnumerable<Ship> GetAll();
        Result<Ship> Create(Ship ship);
        Maybe<Ship> DeleteById(Guid id);
        Result<Ship> Update(Ship ship);
    }
}
