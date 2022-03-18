using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using Entities.Model.Workers;

namespace Entities.Model.TransportStates
{
    public class CanceledTransport : TransportState
    {
        public CanceledTransport(Transport transport) : base(transport) { }
        public static Result<CanceledTransport> Create(Transport transport)
        {
            if (transport == null)
            {
                return Result.Failure<CanceledTransport>("You didn't setted transport !");
            }
            Result<CanceledTransport> result = new CanceledTransport(transport);
            return result;
        }
        protected override Result<Ship> AddShip(Ship ship)
        {
            this.StateChangeCheck();
            return Result.Failure<Ship>("You can't change ship while transport is canceled !");
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(ICollection<ShipCaptain> shipCaptain)
        {
            this.StateChangeCheck();
            return Result.Failure<ICollection<ShipCaptain>>("You can't change ship captains while transport is canceled !");
        }

        protected override Result<ICollection<Crew>> AddShipCrew(ICollection<Crew> crew)
        {
            this.StateChangeCheck();
            return Result.Failure<ICollection<Crew>>("You can't change ship crew while transport is canceled !");
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(ShipCaptain shipCaptain)
        {
            this.StateChangeCheck();
            return Result.Failure<ShipCaptain>("You can't change ship captain while transport is canceled !");
        }
    }
}
