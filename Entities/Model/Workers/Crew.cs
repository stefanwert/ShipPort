using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model.Workers
{
    public class Crew: Worker
    {
        public int SailingHoursTotal { get; }

        public CrewRole Role { get; }
    }
}
