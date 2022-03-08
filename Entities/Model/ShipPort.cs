using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Entities.Model.Workers;

namespace Entities.Model
{
    public class ShipPort
    {

        public Guid Id { get; }

        public string Name { get; }

        public DateTime TimeOfCreation { get; }

        public ICollection<Worker> Workers { get; }

        public ICollection<Ship> Ships { get; }

        public ICollection<Warehouse> Warehouses { get; }

        private ShipPort(Guid id, string name, DateTime timeOfCreation, ICollection<Worker> workers, ICollection<Ship> ships, ICollection<Warehouse> warehouses)
        {
            Id = id;
            Name = name;
            TimeOfCreation = timeOfCreation;
            Workers = workers;
            Ships = ships;
            Warehouses = warehouses;
        }

        public static Result<ShipPort> Create(Guid id, string name, DateTime timeOfCreation, ICollection<Worker> workers, ICollection<Ship> ships, ICollection<Warehouse> warehouses)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<ShipPort>("Name is not setted !");
            }
            if (timeOfCreation == null)
            {
                return Result.Failure<ShipPort>("Time of creation is not setted !");
            }
            if (workers == null)
            {
                return Result.Failure<ShipPort>("Workers are not setted !");
            }
            if (ships == null)
            {
                return Result.Failure<ShipPort>("Ships are not setted !");
            }
            if (warehouses == null)
            {
                return Result.Failure<ShipPort>("Warehouses are not setted !");
            }
            Result<ShipPort> result = new ShipPort(id, name, timeOfCreation, workers, ships, warehouses);
            return result;
        }
    }
}
