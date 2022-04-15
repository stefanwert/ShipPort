using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class ShipPortDTO
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTime TimeOfCreation { get; private set; }

        public ICollection<WorkerDTO> Workers { get; private set; }

        public ICollection<ShipDTO> Ships { get; private set; }

        public ICollection<WarehouseDTO> Warehouses { get; private set; }
    }
}
