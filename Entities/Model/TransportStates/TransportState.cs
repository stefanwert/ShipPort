using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Entities.Model.Workers;

namespace Entities.Model.TransportStates
{
    public abstract class TransportState
    {

        protected TransportState() { }
        protected abstract Result<ICollection<ShipCaptain>> AddShipCaptain(Transport transport, ICollection<ShipCaptain> shipCaptain);

        protected abstract Result<ICollection<Crew>> AddShipCrew(Transport transport, ICollection<Crew> crew);

        protected abstract Result<Ship> AddShip(Transport transport, Ship ship);

        protected abstract Result<ShipCaptain> SetCurrentShipCaptain(Transport transport, ShipCaptain shipCaptain);
        protected void StateChangeCheck(Transport transport)
        {
            if (transport.IsTimeToTransport() && !transport.IsTransportReady())
            {
                Result<CanceledTransport> result = CanceledTransport.Create();
                if (result.IsSuccess)
                {
                    transport.TransportState = result.Value;
                }
            }
            else if (transport.IsCurrentStateCreateing() && transport.IsTimeToTransport())
            {
                Result<Transporting> result = Transporting.Create();
                if (result.IsSuccess)
                {
                    transport.TransportState = result.Value;
                }
            }
            else if (transport.IsCurrentStateCanceled() && !transport.IsTimeToTransport())
            {
                Result<CreatingTransport> result = CreatingTransport.Create();
                if (result.IsSuccess)
                {
                    transport.TransportState = result.Value;
                }
            }
        }
        public abstract string ToString();

    }
}
