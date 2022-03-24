using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model.Workers
{
    public class Crew: Worker
    {
        public int SailingHoursTotal { get; private set; }

        public CrewRole Role { get; private set; }

        private Crew():base() { }
        private Crew(int sailingHoursTotal, CrewRole role, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable)
            :base(id, name, surname, age, yearsOfWorking, salary, isAvailable)
        {
            SailingHoursTotal = sailingHoursTotal;
            Role = role;
        }
    }
}
