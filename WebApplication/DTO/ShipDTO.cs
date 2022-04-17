using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class ShipDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public ShipPortDTO ShipPort { get; set; }
    }
}
