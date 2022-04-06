using Core.Model.Workers;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repository
{
    public interface IShipCaptainRepository
    {
        Maybe<ShipCaptain> FindById(Guid id);
        IEnumerable<ShipCaptain> GetAll();
        Result<ShipCaptain> Create(ShipCaptain shipCaptain);
        Maybe<ShipCaptain> DeleteById(Guid id);
        Result<ShipCaptain> Update(ShipCaptain shipCaptain);
    }
}
