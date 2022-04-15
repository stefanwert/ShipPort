using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class ShipDTO
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public float Price { get; private set; }

        public ShipPortDTO ShipPort { get; private set; }
    }
}
