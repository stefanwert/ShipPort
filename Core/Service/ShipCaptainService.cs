using Core.Model;
using Core.Model.Workers;
using Core.Repository;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Service
{
    public class ShipCaptainService
    {
        private readonly IShipCaptainRepository ShipCaptainRepository;

        public ShipCaptainService(IShipCaptainRepository shipCaptainRepository)
        {
            ShipCaptainRepository = shipCaptainRepository;
        }

        public Result<ShipCaptain> Create(ShipCaptain shipCaptain)
        {
            Result<ShipCaptain> ret = ShipCaptainRepository.Create(shipCaptain);
            return Result.Success(ret.Value);
        }

        public Maybe<ShipCaptain> DeleteById(Guid id)
        {
            Maybe<ShipCaptain> shipCaptain = FindById(id);
            if (shipCaptain.HasNoValue)
                return Maybe.None;
            return ShipCaptainRepository.DeleteById(id);
        }

        public Maybe<ShipCaptain> FindById(Guid id)
        {
            var shipCaptain = ShipCaptainRepository.FindById(id);
            return shipCaptain == null ? Maybe.None : shipCaptain;
        }

        public IEnumerable<ShipCaptain> GetAll()
        {
            return ShipCaptainRepository.GetAll();
        }

        public Result<ShipCaptain> Update(ShipCaptain shipCaptain)
        {
            Result<ShipCaptain> ret = ShipCaptainRepository.Update(shipCaptain);
            return Result.Success(ret.Value);
        }

        public void ChangeShipCaptainsLocation(Transport transport)
        {
            var shipPort = transport.ShipPortTo;
            var numberOfShipCaptains = transport.ShipCaptains.Count;
            foreach(var shipCaptain in transport.ShipCaptains ?? Enumerable.Empty<ShipCaptain>())
            {
                var transportTime = transport.TimeTo.Subtract(transport.TimeFrom);
                var transportTimeInHours = transportTime.TotalHours;
                var SailingHoursTotalNew = shipCaptain.SailingHoursTotal + transportTimeInHours;
                var sailingHoursAsCaptain = shipCaptain.SailingHoursAsCaptain + transportTimeInHours / numberOfShipCaptains;
                Result<ShipCaptain> result = ShipCaptain.Create(SailingHoursTotalNew, sailingHoursAsCaptain, shipCaptain.Id, shipCaptain.Name, shipCaptain.Surname, shipCaptain.Age, shipCaptain.YearsOfWorking, shipCaptain.Salary, true, shipPort);

                ShipCaptainRepository.Update(shipCaptain);
            }
        }
    }
}
