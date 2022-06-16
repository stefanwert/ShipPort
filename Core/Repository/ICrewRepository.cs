using System;
using System.Collections.Generic;
using System.Text;
using Core.Model.Workers;
using CSharpFunctionalExtensions;

namespace Core.Repository
{
    public interface ICrewRepository
    {
        Maybe<Crew> FindById(Guid id);
        IEnumerable<Crew> GetAll();
        Result<Crew> Create(Crew crew);
        Maybe<Crew> DeleteById(Guid id);
        Result<Crew> Update(Crew crew);
        ICollection<Crew> FindByShipPortId(Guid id);
    }
}
