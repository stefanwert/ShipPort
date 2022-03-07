using System;

namespace Entities.Model
{
    public class Ship
    {
        public Guid Id { get; }

        public string Name { get; }

        public float Price { get; }

        public ShipPort ShipPort { get; }
    }
}
