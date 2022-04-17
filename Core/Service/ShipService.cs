using Core.Model;
using Core.Repository;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace Core.Service
{
    public class ShipService
    {
        private IShipRepository ShipRepository;

        public ShipService(IShipRepository shipRepository)
        {
            ShipRepository = shipRepository;
        }

        public Result<Ship> Create(Ship ship)
        {
            Result<Ship> ret = ShipRepository.Create(ship);
            return Result.Success(ret.Value);
        }

        public Maybe<Ship> DeleteById(Guid id)
        {
            Maybe<Ship> ship = FindById(id);
            if (ship.HasNoValue)
                return Maybe.None;
            return ShipRepository.DeleteById(id);
        }

        public Maybe<Ship> FindById(Guid id)
        {
            var ship = ShipRepository.FindById(id);
            return ship == null ? Maybe.None : ship;
        }

        public IEnumerable<Ship> GetAll()
        {
            return ShipRepository.GetAll();
        }

        public Result<Ship> Update(Ship ship)
        {
            Result<Ship> ret = ShipRepository.Update(ship);
            return Result.Success(ret.Value);
        }
    }
}
