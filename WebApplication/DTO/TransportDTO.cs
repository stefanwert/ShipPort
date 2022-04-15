using Core.Model;
using Core.Model.TransportStates;
using Core.Model.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class TransportDTO
    {
        public Guid Id { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public Ship Ship { get; set; }

        public ICollection<ShipCaptain> ShipCaptains { get; set; }

        public ShipCaptain CurrentShipCaptain { get; set; }

        public ICollection<Crew> Crew { get; set; }

        public ShipPort ShipPortFrom { get; set; }

        public ShipPort ShipPortTo { get; set; }

        public string TransportState { get; set; }
    }
}
