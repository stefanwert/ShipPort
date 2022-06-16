using System;
using System.Collections.Generic;
using System.Text;
using Core.Model.Workers;
using Core.Repository;
using CSharpFunctionalExtensions;

namespace Core.Service
{
    public class CrewService
    {
        private readonly ICrewRepository CrewRepository;

        public CrewService(ICrewRepository crewRepository)
        {
            CrewRepository = crewRepository;
        }

        public Result<Crew> Create(Crew crew)
        {
            Result<Crew> ret = CrewRepository.Create(crew);
            return Result.Success(ret.Value);
        }

        public Maybe<Crew> DeleteById(Guid id)
        {
            Maybe<Crew> Crew = FindById(id);
            if (Crew.HasNoValue)
                return Maybe.None;
            return CrewRepository.DeleteById(id);
        }

        public Maybe<Crew> FindById(Guid id)
        {
            var crew = CrewRepository.FindById(id);
            return crew == null ? Maybe.None : crew;
        }

        public IEnumerable<Crew> GetAll()
        {
            return CrewRepository.GetAll();
        }

        public Result<Crew> Update(Crew crewMember)
        {
            Result<Crew> ret = CrewRepository.Update(crewMember);
            return Result.Success(ret.Value);
        }

        public ICollection<Crew> FindByShipPortId(Guid id)
        {
            return CrewRepository.FindByShipPortId(id);
        }
    }
}
