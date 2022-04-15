using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Core.Model.Workers;

namespace Core.Model.TransportStates
{
    public class Transporting : TransportState
    {
        private Transporting() : base() { }

        public static readonly string Name = "Transporting";

        public static Result<Transporting> Create()
        {
            
            Result<Transporting> result = new Transporting();
            return result;
        }
        protected override Result<Ship> AddShip(Transport transport, Ship ship)
        {
            this.StateChangeCheck(transport);
            return Result.Failure<Ship>("You can't change ship while transporting !");
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(Transport transport, ICollection<ShipCaptain> shipCaptains)
        {
            this.StateChangeCheck(transport);
            return Result.Failure<ICollection<ShipCaptain>>("You can't change ship captain while transporting !");
        }

        protected override Result<ICollection<Crew>> AddShipCrew(Transport transport, ICollection<Crew> crew)
        {
            this.StateChangeCheck(transport);
            return Result.Failure<ICollection<Crew>>("You can't change ship crew while transporting !");
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(Transport transport, ShipCaptain shipCaptain)
        {
            this.StateChangeCheck(transport);
            Result<Transport> transportRet = Transport.Create(transport.Id, transport.TimeFrom, transport.TimeTo,
                transport.Ship, transport.ShipCaptains, transport.Crew, transport.ShipPortFrom, transport.ShipPortTo, this.ToString(), shipCaptain);
            if (transportRet.IsFailure)
            {
                return Result.Failure<ShipCaptain>(transportRet.Error);
            }
            return Result.Success(transportRet.Value.CurrentShipCaptain);
        }

        public override string ToString()
        {
            return Name;

        }
    }
}
