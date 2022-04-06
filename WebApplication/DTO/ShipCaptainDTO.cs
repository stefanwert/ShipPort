using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class ShipCaptainDTO :WorkerDTO
    {
        public int SailingHoursTotal { get; set; }

        public int SailingHoursAsCaptain { get; set; }
    }
}
