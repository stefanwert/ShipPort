using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model.Workers
{
    public class ShipCaptain: Worker
    {
        public int SailingHoursTotal { get; private set; }

        public int SailingHoursAsCaptain { get; private set; }

        private ShipCaptain() : base() { }
        private ShipCaptain(int sailingHoursTotal, int sailingHoursAsCaptain, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable):
            base(id, name, surname, age, yearsOfWorking, salary, isAvailable)
        {
            SailingHoursTotal = sailingHoursTotal;
            SailingHoursAsCaptain = sailingHoursAsCaptain;
        }
    }
}
