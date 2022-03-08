using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Entities.Model.Workers;

namespace Entities.Model.TransportStates
{
    public class Transporting : TransportState
    {
        private Transporting(Transport transport) : base(transport) { }

        public static Result<Transporting> Create(Transport transport)
        {
            if(transport == null)
            {
                return Result.Failure<Transporting>("You didn't setted transport !");
            }
            Result<Transporting> result = new Transporting(transport);
            return result;
        }
        protected override Result<Ship> AddShip(Ship ship)
        {
            this.StateChangeCheck();
            return Result.Failure<Ship>("You can't change ship while transporting !");
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(ICollection<ShipCaptain> shipCaptains)
        {
            this.StateChangeCheck();
            return Result.Failure<ICollection<ShipCaptain>>("You can't change ship captain while transporting !");
        }

        protected override Result<ICollection<Crew>> AddShipCrew(ICollection<Crew> crew)
        {
            this.StateChangeCheck();
            return Result.Failure<ICollection<Crew>>("You can't change ship crew while transporting !");
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(ShipCaptain shipCaptain)
        {
            this.StateChangeCheck();
            Result<Transport> transport = Transport.Create(this.Transport.Id, this.Transport.TimeFrom, this.Transport.TimeTo,
                this.Transport.Ship, this.Transport.ShipCaptains, this.Transport.Crew, this.Transport.ShipPortFrom, this.Transport.ShipPortTo, this, shipCaptain);
            if (transport.IsFailure)
            {
                return Result.Failure<ShipCaptain>(transport.Error);
            }
            return Result.Success(transport.Value.CurrentShipCaptain);
        }
    }
}
