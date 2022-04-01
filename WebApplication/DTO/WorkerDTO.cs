using System;

namespace WebShipPort.DTO
{
    public abstract class WorkerDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public int YearsOfWorking { get; set; }

        public long Salary { get; set; }

        public bool IsAvailable { get; set; }
    }
}
