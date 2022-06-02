using Core.Model.TransportStates;
using Core.Model.Workers;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Model
{
    public class Transport
    {
        public Guid Id { get; private set; }

        [Required]
        public DateTime TimeFrom { get; private set; }

        [Required]
        public DateTime TimeTo { get; private set; }

        [Required]
        public Ship Ship { get; private set; }

        public ICollection<ShipCaptain> ShipCaptains { get; private set; }

        public ShipCaptain CurrentShipCaptain { get; private set; }

        public ICollection<Crew> Crew { get; private set; }

        [Required]
        public ShipPort ShipPortFrom { get; private set; }

        [Required]
        public ShipPort ShipPortTo { get; private set; }

        [Required]
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
            TransportState = transportState;
            CurrentShipCaptain = currentShipCaptain;
        }

        public static Result<Transport> Create(Guid id, DateTime timeFrom, DateTime timeTo, Ship ship, ICollection<ShipCaptain> shipCaptains,
            ICollection<Crew> crew, ShipPort shipPortFrom, ShipPort shipPortTo, string transportStateString, ShipCaptain currentShipCaptain)
        {
            if (timeFrom > timeTo)
            {
                return Result.Failure<Transport>("Choose start date that is earlier than end date !!!");
            }
            if (ship == null || shipCaptains == null || shipPortFrom == null || shipPortTo == null || crew == null)
            {
                return Result.Failure<Transport>("Some property is not seted !!");
            }
            if (string.IsNullOrWhiteSpace(transportStateString))
            {
                return Result.Failure<Transport>("Transport state is not setted !");
            }
            TransportState transportState = ConvertStringToTransportState(transportStateString);
            Result<Transport> transport = new Transport(id, timeFrom, timeTo, ship, shipCaptains, crew, shipPortFrom, shipPortTo, transportState, currentShipCaptain);
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
            return !(Ship == null 
                || ShipCaptains == null 
                || ShipPortFrom == null 
                || ShipPortTo == null 
                || TimeFrom == null || TimeTo == null || Crew == null);
            //if (Ship == null || ShipCaptains == null || ShipPortFrom == null ||
            //    ShipPortTo == null || TimeFrom == null || TimeTo == null || Crew == null)
            //{
            //    return false;
            //}
            //return true;
        }

        public bool IsTimeToTransport()
        {
            return TimeFrom > DateTime.Now && TimeTo < DateTime.Now;
        }

        public static TransportState ConvertStringToTransportState(string transportState)
        {
            var canceledTransport = CanceledTransport.Create();
            var transporting = Transporting.Create();
            var createingTransport = CreatingTransport.Create();

            if (canceledTransport.IsFailure || transporting.IsFailure || createingTransport.IsFailure)
                return null;

            return CanceledTransport.Name.Equals(transportState) 
                ? canceledTransport.Value 
                :Transporting.Name.Equals(transportState) 
                    ? transporting.Value 
                    :CreatingTransport.Name.Equals(transportState) 
                        ? createingTransport.Value 
                        :null;
        }
    }
}
