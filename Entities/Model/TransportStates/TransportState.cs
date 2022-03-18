using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Entities.Model.Workers;

namespace Entities.Model.TransportStates
{
    public abstract class TransportState
    {
        protected Transport Transport { get; }

        protected TransportState(Transport transport)
        {
            Transport = transport;
        }
        protected abstract Result<ICollection<ShipCaptain>> AddShipCaptain(ICollection<ShipCaptain> shipCaptain);

        protected abstract Result<ICollection<Crew>> AddShipCrew(ICollection<Crew> crew);

        protected abstract Result<Ship> AddShip(Ship ship);

        protected abstract Result<ShipCaptain> SetCurrentShipCaptain(ShipCaptain shipCaptain);

        protected void StateChangeCheck()
        {
            if (IsTimeToTransport() && !IsTransportReady())
            {
                Result<CanceledTransport> result = CanceledTransport.Create(Transport);
                if (result.IsSuccess)
                {
                    Transport.TransportState = result.Value;
                }
            }
            else if (IsCurrentStateCreateing() && IsTimeToTransport())
            {
                Result<Transporting> result = Transporting.Create(Transport);
                if (result.IsSuccess)
                {
                    Transport.TransportState = result.Value;
                }
            }
            else if (IsCurrentStateCanceled() && !IsTimeToTransport())
            {
                Result<CreateingTransport> result = CreateingTransport.Create(Transport);
                if (result.IsSuccess)
                {
                    Transport.TransportState = result.Value;
                }
            }
        }
        private bool IsTransportReady()
        {
            if (this.Transport.Ship == null || this.Transport.ShipCaptains == null || this.Transport.ShipPortFrom == null ||
                this.Transport.ShipPortTo == null || this.Transport.TimeFrom == null || this.Transport.TimeTo == null || this.Transport.Crew == null)
            {
                return false;
            }
            return true;
        }
        private bool IsCurrentStateTransporting()
        {
            return Transport.TransportState is Transporting;
        }
        private bool IsCurrentStateCanceled()
        {
            return Transport.TransportState is CanceledTransport;
        }
        private bool IsCurrentStateCreateing()
        {
            return Transport.TransportState is CreateingTransport;
        }
        private bool IsTimeToTransport()
        {
            return this.Transport.TimeFrom > DateTime.Now && this.Transport.TimeTo < DateTime.Now;
        }
    }
}
