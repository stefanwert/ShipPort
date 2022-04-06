﻿using CSharpFunctionalExtensions;
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

        public static Result<ShipCaptain> Create(int sailingHoursTotal, int sailingHoursAsCaptain, Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable)
        {
            if (sailingHoursTotal < 0)
            {
                return Result.Failure<ShipCaptain>("SailingHoursTotal cant be lower then 0 !");
            }
            if (sailingHoursAsCaptain < 0)
            {
                return Result.Failure<ShipCaptain>("sailingHoursAsCaptain cant be lower then 0 !");
            }
            if (string.IsNullOrEmpty(name))
            {
                return Result.Failure<ShipCaptain>("Name is not setted !");
            }
            if (string.IsNullOrEmpty(surname))
            {
                return Result.Failure<ShipCaptain>("Surname is not setted !");
            }
            if (age == null || age < 0)
            {
                return Result.Failure<ShipCaptain>("Age is not setted or it is negative!");
            }
            if (yearsOfWorking == null || yearsOfWorking < 0)
            {
                return Result.Failure<ShipCaptain>("Years of working is not setted or it is negative!");
            }
            if (salary == null || salary < 0)
            {
                return Result.Failure<ShipCaptain>("Salary is not setted or it is negative!");
            }
            if (isAvailable == null)
            {
                return Result.Failure<ShipCaptain>("Is available is not setted or it is negative!");
            }
            Result<ShipCaptain> shipCaptain = new ShipCaptain(sailingHoursTotal, sailingHoursAsCaptain, id, name, surname, age, yearsOfWorking, salary, isAvailable);
            return shipCaptain;
        }
    }
}
