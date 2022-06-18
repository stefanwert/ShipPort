using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Workers;

namespace WebShipPort.DTO
{
    public class WarehouseClerkDTO : WorkerDTO
    {
        public ClerkRole ClerkRole { get; set; }

        public Guid WarehouseId { get; set; }

        public WarehouseClerkDTO() { }

        public WarehouseClerkDTO(WarehouseClerk warehouseClerk) : base(warehouseClerk)
        {
            ClerkRole = warehouseClerk.ClerkRole;
            WarehouseId = warehouseClerk.WarehouseId;
            ClerkRole = warehouseClerk.ClerkRole;
        }
    }
}
