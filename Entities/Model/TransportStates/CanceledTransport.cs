using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using Entities.Model.Workers;

namespace Entities.Model.TransportStates
{
    class CanceledTransport : TransportState
    {
        public CanceledTransport(Transport transport) : base(transport) { }

        protected override Result<Ship> AddShip(Ship ship)
        {
            throw new NotImplementedException();
        }

        protected override Result<ICollection<ShipCaptain>> AddShipCaptain(ICollection<ShipCaptain> shipCaptain)
        {
            throw new NotImplementedException();
        }

        protected override Result<ICollection<Crew>> AddShipCrew(ICollection<Crew> crew)
        {
            throw new NotImplementedException();
        }

        protected override Result<ShipCaptain> SetCurrentShipCaptain(ShipCaptain shipCaptain)
        {
            throw new NotImplementedException();
        }
    }
}
