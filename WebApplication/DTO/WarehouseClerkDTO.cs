using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Workers;

namespace WebShipPort.DTO
{
    public class WarehouseClerkDTO : WorkerDTO
    {
        public WarehouseClerkDTO(): base() { }

        public ClerkRole ClerkRole { get; set; }
    }
}
