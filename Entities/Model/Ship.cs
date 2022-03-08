﻿using System;
using CSharpFunctionalExtensions;

namespace Entities.Model
{
    public class Ship
    {
        public Guid Id { get; }

        public string Name { get; }

        public float Price { get; }

        public ShipPort ShipPort { get; }

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
            if (shipPort == null)
            {
                return Result.Failure<Ship>("Ship port is not setted !");
            }
            Result<Ship> result = new Ship(id, name, price, shipPort);
            return result;
        }
    }
}
