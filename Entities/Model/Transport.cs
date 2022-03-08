using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Entities.Model.TransportStates;
using Entities.Model.Workers;

namespace Entities.Model
{
    public class Transport
    {
        public Guid Id { get; }

        public DateTime TimeFrom { get; }

        public DateTime TimeTo { get; }

        public Ship Ship { get; }

        public ICollection<ShipCaptain> ShipCaptains { get; }

        public ShipCaptain CurrentShipCaptain { get; }

        public ICollection<Crew> Crew { get; }

        public ShipPort ShipPortFrom { get; }

        public ShipPort ShipPortTo { get; }

        public TransportState TransportState { get; set; }

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
            TransportState = transportState;
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
            return transport;
        }

    }
}
