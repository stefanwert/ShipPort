using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Entities.Model.Workers;

namespace Entities.Model.TransportStates
{
    public class CreateingTransport : TransportState
    {
        private CreateingTransport(Transport transport) : base(transport) { }

        public static Result<CreateingTransport> Create(Transport transport)
        {
            if (transport == null)
            {
                return Result.Failure<CreateingTransport>("You didn't setted transport !");
            }
            Result<CreateingTransport> result = new CreateingTransport(transport);
            return result;
        }

        protected override Result<Ship> AddShip(Ship ship)
        {
            this.StateChangeCheck();
            Result<Transport> transport = Transport.Create(this.Transport.Id, this.Transport.TimeFrom, this.Transport.TimeTo,
                ship, this.Transport.ShipCaptains, this.Transport.Crew, this.Transport.ShipPortFrom, this.Transport.ShipPortTo, this, this.Transport.CurrentShipCaptain);
            if (transport.IsFailure)
            {
                return Result.Failure<Ship>(transport.Error);
            }
            return Result.Success(transport.Value.Ship);
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(ICollection<ShipCaptain> shipCaptains)
        {
            this.StateChangeCheck();
            Result<Transport> transport = Transport.Create(this.Transport.Id, this.Transport.TimeFrom, this.Transport.TimeTo,
                this.Transport.Ship, shipCaptains, this.Transport.Crew, this.Transport.ShipPortFrom, this.Transport.ShipPortTo, this, this.Transport.CurrentShipCaptain);
            if (transport.IsFailure)
            {
                return Result.Failure<ICollection<ShipCaptain>>(transport.Error);
            }
            return Result.Success(transport.Value.ShipCaptains);
        }

        protected override Result<ICollection<Crew>> AddShipCrew(ICollection<Crew> crew)
        {
            this.StateChangeCheck();
            Result<Transport> transport = Transport.Create(this.Transport.Id, this.Transport.TimeFrom, this.Transport.TimeTo,
                this.Transport.Ship, this.Transport.ShipCaptains, this.Transport.Crew, this.Transport.ShipPortFrom, this.Transport.ShipPortTo, this, this.Transport.CurrentShipCaptain);
            if (transport.IsFailure)
            {
                return Result.Failure<ICollection<Crew>>(transport.Error);
            }
            return Result.Success(transport.Value.Crew);
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(ShipCaptain shipCaptain)
        {
            this.StateChangeCheck();
            return Result.Failure<ShipCaptain>("You can't change ship captain while createing transport");
        }
    }
}
