using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class ShipPortDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime TimeOfCreation { get; set; }

        public ICollection<WorkerDTO> Workers { get; set; }

        public ICollection<ShipDTO> Ships { get; set; }

        public ICollection<WarehouseDTO> Warehouses { get; set; }
    }
}
