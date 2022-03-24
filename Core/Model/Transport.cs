using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Core.Model.TransportStates;
using Core.Model.Workers;

namespace Core.Model
{
    public class Transport
    {
        public Guid Id { get; private set; }

        public DateTime TimeFrom { get; private set; }

        public DateTime TimeTo { get; private set; }

        public Ship Ship { get; private set; }

        public ICollection<ShipCaptain> ShipCaptains { get; private set; }

        public ShipCaptain CurrentShipCaptain { get; private set; }

        public ICollection<Crew> Crew { get; private set; }

        public ShipPort ShipPortFrom { get; private set; }

        public ShipPort ShipPortTo { get; private set; }

        public TransportState TransportState { get; set; }

        private Transport() { }

        private Transport(Guid id, DateTime timeFrom, DateTime timeTo, Ship ship, ICollection<ShipCaptain> shipCaptains,
            ICollection<Crew> crew, ShipPort shipPortFrom, ShipPort shipPortTo, TransportState transportState, ShipCaptain currentShipCaptain)
        {
            Id = id;
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Ship = ship;
            ShipCaptains = shipCaptains;
            Crew = crew;
            ShipPortFrom = shipPortFrom;
            ShipPortTo = shipPortTo;
            //TransportState = transportState;
            CurrentShipCaptain = currentShipCaptain;
        }

        public static Result<Transport> Create(Guid id, DateTime timeFrom, DateTime timeTo, Ship ship, ICollection<ShipCaptain> shipCaptains,
            ICollection<Crew> crew, ShipPort shipPortFrom, ShipPort shipPortTo, TransportState transportState, ShipCaptain currentShipCaptain)
        {
            if (timeFrom == null || timeTo == null)
            {
                return Result.Failure<Transport>("Time is not seted !!!");
            }
            if(timeFrom > timeTo)
            {
                return Result.Failure<Transport>("Choose start date that is earlier than end date !!!");
            }
            if (ship == null || shipCaptains == null || shipPortFrom == null || shipPortTo == null || crew == null)
            {
                return Result.Failure<Transport>("Some property is not seted !!");
            }
            Result<Transport> transport = new Transport(id, timeFrom, timeTo, ship, shipCaptains, crew, shipPortFrom, shipPortTo, transportState, currentShipCaptain);
            if(transportState == null)
            {
                var state = CreatingTransport.Create();
                if (state.IsSuccess)
                {
                    transport.Value.TransportState = state.Value;
                }
            }
            
            return transport;
        }

        public bool IsCurrentStateTransporting()
        {
            return TransportState is Transporting;
        }
        public bool IsCurrentStateCanceled()
        {
            return TransportState is CanceledTransport;
        }
        public bool IsCurrentStateCreateing()
        {
            return TransportState is CreatingTransport;
        }
        
        public bool IsTransportReady()
        {
            if (Ship == null || ShipCaptains == null || ShipPortFrom == null ||
                ShipPortTo == null || TimeFrom == null || TimeTo == null || Crew == null)
            {
                return false;
            }
            return true;
        }

        public bool IsTimeToTransport()
        {
            return TimeFrom > DateTime.Now && TimeTo < DateTime.Now;
        }

    }
}
