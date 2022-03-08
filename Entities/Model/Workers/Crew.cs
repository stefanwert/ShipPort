using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Model.Workers
{
    public class Crew: Worker
    {
        public int SailingHoursTotal { get; }

        public CrewRole Role { get; }

        public Crew(int sailingHoursTotal, CrewRole role, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable)
            :base(id, name, surname, age, yearsOfWorking, salary, isAvailable)
        {
            SailingHoursTotal = sailingHoursTotal;
            Role = role;
        }
    }
}
