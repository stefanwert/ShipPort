using System;
using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Core.Model
{
    public class Ship
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public float Price { get; private set; }

        [Required]
        public Guid ShipPortId { get; private set; }

        private Ship() { }
        private Ship(Guid id, string name, float price, Guid shipPortId)
        {
            Id = id;
            Name = name;
            Price = price;
            ShipPortId = shipPortId;
        }

        public static Result<Ship> Create(Guid id, string name, float price, Guid shipPortId)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Ship>("Name is not setted !");
            }
            if (price == null || price < 0)
            {
                return Result.Failure<Ship>("Price is not setted or it is negative !");
            }
            
            Result<Ship> result = new Ship(id, name, price, shipPortId);
            return result;
        }
    }
}
