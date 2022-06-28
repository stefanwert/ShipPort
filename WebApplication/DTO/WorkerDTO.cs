using Core.Model.Workers;
using System;

namespace WebShipPort.DTO
{
    public class WorkerDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public int YearsOfWorking { get; set; }

        public long Salary { get; set; }

        public bool IsAvailable { get; set; }

        public Guid ShipPortId { get; set; }
        public WorkerDTO() { }
        public WorkerDTO(Worker worker)
        {
            Id = worker.Id;
            Name = worker.Name;
            Surname = worker.Surname;
            Age = worker.Age;
            YearsOfWorking = worker.YearsOfWorking;
            Salary = worker.Salary;
            IsAvailable = worker.IsAvailable;
            ShipPortId = worker.ShipPortId;
        }
    }
}
