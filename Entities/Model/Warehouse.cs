using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model
{
    public class Warehouse
    {
        public Guid Id { get; }
        public string Name { get; }
        public bool StoreFlammableCargo { get; }
        public int CargoCapacity { get; }
    }
}
