﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Model.Workers
{
    public class Worker
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public int Age { get; private set; }

        public int YearsOfWorking { get; private set; }

        public long Salary { get; private set; }

        public bool IsAvailable { get; private set; }

        protected Worker() { }

        protected Worker(Guid id, string name, string surname, int age, int yearsOfWorking, long salary, bool isAvailable)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Age = age;
            YearsOfWorking = yearsOfWorking;
            Salary = salary;
            IsAvailable = isAvailable;
        }

    }
}
