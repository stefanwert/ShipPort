using System;
using CSharpFunctionalExtensions;

namespace Entities.Model.Workers
{
    public class Worker
    {
        public Guid Id { get; }

        public string Name { get; }
        
        public string Surname { get; }
        
        public int Age { get; }

        public int YearsOfWorking { get; }

        public long Salary { get; }

        public bool IsAvailable { get; }

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
