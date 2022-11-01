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
        public DateTime TimeFrom { get; set; }

        [Required]
        public DateTime TimeTo { get;  set; }

        [Required]
        public Ship Ship { get; set; }

        public virtual ICollection<ShipCaptain> ShipCaptains { get; set; }

        public ShipCaptain CurrentShipCaptain { get; set; }
        public Guid? CurrentShipCaptainId { get; set; }


        public ICollection<Crew> Crew { get; set; }

        [Required]
        public virtual ShipPort ShipPortFrom { get;  set; }
        //public Guid? ShipPortFromId { get; private set; }

        [Required]
        public virtual ShipPort ShipPortTo { get;  set; }

        //public Guid? ShipPortToId { get; private set; }

        //public Guid? ShipId { get; private set; }

        [Required]
        public virtual TransportState TransportState { get; set; }

        public virtual ICollection<Cargo> Cargos { get; set; }

        private Transport() { }

        private Transport(Guid id, DateTime timeFrom, DateTime timeTo, Ship ship, ICollection<ShipCaptain> shipCaptains,
            ICollection<Crew> crew, ShipPort shipPortFrom, ShipPort shipPortTo, TransportState transportState, ShipCaptain currentShipCaptain)
        {
            Id = id;
            TimeFrom = timeFrom;
            TimeTo = timeTo;
            Ship = ship;
            ShipPortFrom = shipPortFrom;
            ShipPortTo = shipPortTo;
            ShipCaptains = shipCaptains;
            Crew = crew;
            TransportState = transportState;
            CurrentShipCaptain = currentShipCaptain;
            if (currentShipCaptain != null)
                CurrentShipCaptainId = currentShipCaptain.Id;
            //ShipPortFromId = shipPortFrom.Id;
            //ShipPortToId = shipPortTo.Id;
            //ShipId = ship.Id;
        }

        public static Result<Transport> Create(Guid id, DateTime timeFrom, DateTime timeTo, Ship ship, ICollection<ShipCaptain> shipCaptains,
            ICollection<Crew> crew, ShipPort shipPortFrom, ShipPort shipPortTo, string transportStateString, ShipCaptain currentShipCaptainId)
        {
            if (DateTime.Compare(timeFrom, timeTo) >= 0)
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
            Result<Transport> transport = new Transport(id, timeFrom, timeTo, ship, shipCaptains, crew, shipPortFrom, shipPortTo, transportState, currentShipCaptainId);
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
                || TimeFrom == null || TimeTo == null || Crew == null || Crew?.Count!= 0);
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
            var finished = new FinishedTransport();

            if (canceledTransport.IsFailure || transporting.IsFailure || createingTransport.IsFailure )
                return null;

            return CanceledTransport.Name.Equals(transportState) 
                ? canceledTransport.Value 
                :Transporting.Name.Equals(transportState) 
                    ? transporting.Value 
                    :CreatingTransport.Name.Equals(transportState) 
                        ? createingTransport.Value
                        : FinishedTransport.Name.Equals(transportState)
                            ? finished
                                : null;
        }

        public bool CancelTransport()
        {
            if (TransportState.ToString().Equals(CreatingTransport.Name))
            {
                TransportState = CanceledTransport.Create().Value;
                return true;
            }
            return false;
        }
    }
}
