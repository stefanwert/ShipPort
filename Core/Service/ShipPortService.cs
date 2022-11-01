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
    public class ShipPortService
    {
        private IShipPortRepository ShipPortRepository;

        public ShipPortService(IShipPortRepository shipPortRepository)
        {
            ShipPortRepository = shipPortRepository;
        }
        public Result<ShipPort> Create(ShipPort shipPort)
        {
            Result<ShipPort> ret = ShipPortRepository.Create(shipPort);
            return Result.Success(ret.Value);
        }

        public Maybe<ShipPort> DeleteById(Guid id)
        {
            Maybe<ShipPort> shipPort = FindById(id);
            if (shipPort.HasNoValue)
                return Maybe.None;
            return ShipPortRepository.DeleteById(id);
        }

        public Maybe<ShipPort> FindById(Guid id)
        {
            var shipPort = ShipPortRepository.FindById(id);
            return shipPort == null ? Maybe.None : shipPort;
        }

        public bool ShipPortExist(Guid id)
        {
            return ShipPortRepository.ShipPortExist(id);
        }

        public IEnumerable<ShipPort> GetAll()
        {
            return ShipPortRepository.GetAll();
        }

        public Result<ShipPort> Update(ShipPort shipPort)
        {
            Result<ShipPort> ret = ShipPortRepository.Update(shipPort);
            return Result.Success(ret.Value);
        }

        public bool DoesShipPortContainWarehouse(Guid warehouseId, Guid shipportId)
        {
            return ShipPortRepository.DoesShipPortContainWarehouse(warehouseId, shipportId);
        }

        public IEnumerable<ShipPort> GetAllWithOutRelationships()
        {
            return ShipPortRepository.GetAllWithOutRelationships();
        }
        public Maybe<ShipPort> FindByIdWithOutRelationships(Guid id)
        {
            return ShipPortRepository.FindByIdWithOutRelationships(id);
        }
    }
}
