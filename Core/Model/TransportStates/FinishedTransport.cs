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
            throw new NotImplementedException();
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(Transport transport, ICollection<ShipCaptain> shipCaptain)
        {
            throw new NotImplementedException();
        }

        protected override Result<ICollection<Crew>> AddShipCrew(Transport transport, ICollection<Crew> crew)
        {
            throw new NotImplementedException();
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(Transport transport, ShipCaptain shipCaptain)
        {
            throw new NotImplementedException();
        }
    }
}
