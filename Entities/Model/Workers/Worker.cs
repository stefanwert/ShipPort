using System;

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

    }
}
