using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Entities.Model.Workers;

namespace Entities.Model.TransportStates
{
    public class CreatingTransport : TransportState
    {
        private CreatingTransport() : base() { }

        public static Result<CreatingTransport> Create()
        {
            Result<CreatingTransport> result = new CreatingTransport();
            return result;
        }

        protected override Result<Ship> AddShip(Transport transport, Ship ship)
        {
            this.StateChangeCheck(transport);
            Result<Transport> transportRet = Transport.Create(transport.Id, transport.TimeFrom, transport.TimeTo,
                ship, transport.ShipCaptains, transport.Crew, transport.ShipPortFrom, transport.ShipPortTo, this, transport.CurrentShipCaptain);
            if (transportRet.IsFailure)
            {
                return Result.Failure<Ship>(transportRet.Error);
            }
            return Result.Success(transportRet.Value.Ship);
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(Transport transport, ICollection<ShipCaptain> shipCaptains)
        {
            this.StateChangeCheck(transport);
            Result<Transport> transportRet = Transport.Create(transport.Id, transport.TimeFrom, transport.TimeTo,
                transport.Ship, shipCaptains, transport.Crew, transport.ShipPortFrom, transport.ShipPortTo, this, transport.CurrentShipCaptain);
            if (transportRet.IsFailure)
            {
                return Result.Failure<ICollection<ShipCaptain>>(transportRet.Error);
            }
            return Result.Success(transportRet.Value.ShipCaptains);
        }

        protected override Result<ICollection<Crew>> AddShipCrew(Transport transport, ICollection<Crew> crew)
        {
            this.StateChangeCheck(transport);
            Result<Transport> transportRet = Transport.Create(transport.Id, transport.TimeFrom, transport.TimeTo,
                transport.Ship, transport.ShipCaptains, transport.Crew, transport.ShipPortFrom, transport.ShipPortTo, this, transport.CurrentShipCaptain);
            if (transportRet.IsFailure)
            {
                return Result.Failure<ICollection<Crew>>(transportRet.Error);
            }
            return Result.Success(transportRet.Value.Crew);
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(Transport transport, ShipCaptain shipCaptain)
        {
            this.StateChangeCheck(transport);
            return Result.Failure<ShipCaptain>("You can't change ship captain while createing transport");
        }

        public override string ToString()
        {
            return "CreatingTransport";

        }
    }
}
