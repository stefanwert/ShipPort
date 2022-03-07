using System;
using System.Collections.Generic;
using Entities.Model.Workers;

namespace Entities.Model
{
    public class ShipPort
    {
        public Guid Id { get; }

        public string Name { get; }

        public DateTime TimeOfCreation { get; }

        public IEnumerable<Worker> Workers { get; }

        public IEnumerable<Ship> Ships { get; }

        public IEnumerable<Warehouse> Warehouses { get; }
    }
}
