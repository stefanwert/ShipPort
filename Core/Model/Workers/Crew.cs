using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Model.Workers
{
    public class Crew : Worker
    {
        public int SailingHoursTotal { get; private set; }

        public CrewRole Role { get; private set; }

        private Crew() : base() { }
        private Crew(int sailingHoursTotal, CrewRole role, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable)
            : base(id, name, surname, age, yearsOfWorking, salary, isAvailable)
        {
            SailingHoursTotal = sailingHoursTotal;
            Role = role;
        }

        //add create
        public static Result<Crew> Create(int sailingHoursTotal, CrewRole role, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable)
        {
            if(sailingHoursTotal< 0)
            {
                return Result.Failure<Crew>("SailingHoursTotal cant be lower then 0 !");
            }
            if(role == null)
            {
                return Result.Failure<Crew>("Role cant be null !");
            }
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<Crew>("Name is not setted !");
            }
            if (string.IsNullOrEmpty(surname))
            {
                return Result.Failure<Crew>("Surname is not setted !");
            }
            if (age == null || age < 0)
            {
                return Result.Failure<Crew>("Age is not setted or it is negative!");
            }
            if (yearsOfWorking == null || yearsOfWorking < 0)
            {
                return Result.Failure<Crew>("Years of working is not setted or it is negative!");
            }
            if (salary == null || salary < 0)
            {
                return Result.Failure<Crew>("Salary is not setted or it is negative!");
            }
            if (isAvailable == null)
            {
                return Result.Failure<Crew>("Is available is not setted or it is negative!");
            }
            Result<Crew> crew = new Crew(sailingHoursTotal, role, id, name, surname, age, yearsOfWorking, salary, isAvailable);
            return crew;
        }

    }
}
