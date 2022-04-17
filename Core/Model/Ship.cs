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

        public ShipPort ShipPort { get; private set; }

        private Ship() { }
        private Ship(Guid id, string name, float price, ShipPort shipPort)
        {
            Id = id;
            Name = name;
            Price = price;
            ShipPort = shipPort;
        }

        public static Result<Ship> Create(Guid id, string name, float price, ShipPort shipPort)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Ship>("Name is not setted !");
            }
            if (price == null || price < 0)
            {
                return Result.Failure<Ship>("Price is not setted or it is negative !");
            }
            
            Result<Ship> result = new Ship(id, name, price, shipPort);
            return result;
        }
    }
}
