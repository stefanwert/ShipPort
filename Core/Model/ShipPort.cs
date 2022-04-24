using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Core.Model.Workers;
using System.Text.Json.Serialization;

namespace Core.Model
{
    public class ShipPort
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        [Required]
        public DateTime TimeOfCreation { get; private set; }

        [JsonIgnore]
        public ICollection<Worker> Workers { get; private set; }

        [JsonIgnore]
        public ICollection<Ship> Ships { get; private set; }

        [JsonIgnore]
        public ICollection<Warehouse> Warehouses { get; private set; }

        private ShipPort() { }
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
            //if (workers == null)
            //{
            //    return Result.Failure<ShipPort>("Workers are not setted !");
            //}
            //if (ships == null)
            //{
            //    return Result.Failure<ShipPort>("Ships are not setted !");
            //}
            //if (warehouses == null)
            //{
            //    return Result.Failure<ShipPort>("Warehouses are not setted !");
            //}
            Result<ShipPort> result = new ShipPort(id, name, timeOfCreation, workers, ships, warehouses);
            return result;
        }
    }
}
