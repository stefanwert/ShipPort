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

        public ShipDTO Ship { get; set; }

        public ICollection<ShipCaptainDTO> ShipCaptains { get; set; }

        public ShipCaptainDTO CurrentShipCaptain { get; set; }

        public ICollection<CrewDTO> Crew { get; set; }

        public ShipPortDTO ShipPortFrom { get; set; }

        public ShipPortDTO ShipPortTo { get; set; }

        public string TransportState { get; set; }
    }
}
