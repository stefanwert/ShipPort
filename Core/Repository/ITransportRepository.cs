using Core.Model;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Core.Repository
{
    public interface ITransportRepository
    {
        Maybe<Transport> FindById(Guid id);
        IEnumerable<Transport> GetAll();
        Result<Transport> Create(Transport transport);
        Maybe<Transport> DeleteById(Guid id);
        Result<Transport> Update(Transport transport);
        IEnumerable<Transport> GetAllActive();
        ICollection<Transport> FindByShipPortId(Guid id);
    }
}
