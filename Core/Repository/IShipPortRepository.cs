using Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public interface IShipPortRepository
    {
        Maybe<ShipPort> FindById(Guid id);
        IEnumerable<ShipPort> GetAll();
        Result<ShipPort> Create(ShipPort shipPort);
        Maybe<ShipPort> DeleteById(Guid id);
        Result<ShipPort> Update(ShipPort shipPort);
    }
}
