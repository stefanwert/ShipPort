using Core.Model.Workers;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model.TransportStates
{
    public class FinishedTransport : TransportState
    {
        public FinishedTransport() : base() { }

        public static readonly string Name = "FinishedTransport";

        public override string ToString()
        {
            return Name;
        }

        protected override Result<Ship> AddShip(Transport transport, Ship ship)
        {
            return Result.Failure<Ship>("You can't change ship while Finished !");
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(Transport transport, ICollection<ShipCaptain> shipCaptain)
        {
            return Result.Failure<ICollection<ShipCaptain>>("You can't change add ship captain while Finished !");
        }

        protected override Result<ICollection<Crew>> AddShipCrew(Transport transport, ICollection<Crew> crew)
        {
            return Result.Failure<ICollection<Crew>>("You can't change crew while Finished !");
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(Transport transport, ShipCaptain shipCaptain)
        {
            return Result.Failure<ShipCaptain>("You can't change curret ship captain while Finished !");
        }
    }
}
