using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using Core.Model.Workers;

namespace Core.Model.TransportStates
{
    public class CanceledTransport : TransportState
    {
        public CanceledTransport() : base() { }
        public static Result<CanceledTransport> Create()
        {
            Result<CanceledTransport> result = new CanceledTransport();
            return result;
        }

        protected override Result<Ship> AddShip(Transport transport, Ship ship)
        {
            this.StateChangeCheck(transport);
            return Result.Failure<Ship>("You can't change ship while transport is canceled !");
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(Transport transport, ICollection<ShipCaptain> shipCaptain)
        {
            this.StateChangeCheck(transport);
            return Result.Failure<ICollection<ShipCaptain>>("You can't change ship captains while transport is canceled !");
        }

        protected override Result<ICollection<Crew>> AddShipCrew(Transport transport, ICollection<Crew> crew)
        {
            this.StateChangeCheck(transport);
            return Result.Failure<ICollection<Crew>>("You can't change ship crew while transport is canceled !");
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(Transport transport, ShipCaptain shipCaptain)
        {
            this.StateChangeCheck(transport);
            return Result.Failure<ShipCaptain>("You can't change ship captain while transport is canceled !");
        }

        public override string ToString()
        {
            return "CanceledTransport";
        }
    }
}
