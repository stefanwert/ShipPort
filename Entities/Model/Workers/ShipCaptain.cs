using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model.Workers
{
    public class ShipCaptain: Worker
    {
        public int SailingHoursTotal { get; }

        public int SailingHoursAsCaptain { get; }

        private ShipCaptain(int sailingHoursTotal, int sailingHoursAsCaptain, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable):
            base(id, name, surname, age, yearsOfWorking, salary, isAvailable)
        {
            SailingHoursTotal = sailingHoursTotal;
            SailingHoursAsCaptain = sailingHoursAsCaptain;
        }
    }
}
