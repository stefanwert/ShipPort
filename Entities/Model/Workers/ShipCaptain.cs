using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model.Workers
{
    public class ShipCaptain: Worker
    {
        public int SailingHoursTotal { get; }

        public int SailingHoursAsCaptain { get; }
    }
}
