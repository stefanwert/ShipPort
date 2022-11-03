using Core.Model;
using Core.Repository;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class CargoService
    {
        private readonly ICargoRepository CargoRepository;

        public CargoService(ICargoRepository cargoRepository)
        {
            CargoRepository = cargoRepository;
        }

        public Result<Cargo> Create(Cargo crew)
        {
            Result<Cargo> ret = CargoRepository.Create(crew);
            return Result.Success(ret.Value);
        }

        public Maybe<Cargo> DeleteById(Guid id)
        {
            Maybe<Cargo> Cargo = FindById(id);
            if (Cargo.HasNoValue)
                return Maybe.None;
            return CargoRepository.DeleteById(id);
        }

        public Maybe<Cargo> FindById(Guid id)
        {
            var cargo = CargoRepository.FindById(id);
            return cargo == null ? Maybe.None : cargo;
        }

        public IEnumerable<Cargo> GetAll()
        {
            return CargoRepository.GetAll();
        }

        public IEnumerable<Cargo> GetAllThatIsNotTrasnporting()
        {
            return CargoRepository.GetAllThatIsNotTrasnporting();
        }

        public Result<Cargo> Update(Cargo crewMember)
        {
            Result<Cargo> ret = CargoRepository.Update(crewMember);
            return Result.Success(ret.Value);
        }

        public IEnumerable<Cargo> GetAllFromWarehouse(Guid warehouseId)
        {
            return CargoRepository.GetAllFromWarehouse(warehouseId);
        }
    }
}
