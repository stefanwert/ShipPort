using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using CSharpFunctionalExtensions;

namespace Core.Model
{
    public class Warehouse
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public bool StoreFlammableCargo { get; private set; }

        public int CargoCapacity { get; private set; }

        private Warehouse() { }
        
        private Warehouse(Guid id, string name, bool storeFlammableCargo, int cargoCapacity)
        {
            Id = id;
            Name = name;
            StoreFlammableCargo = storeFlammableCargo;
            CargoCapacity = cargoCapacity;
        }

        public static Result<Warehouse> Create(Guid id, string name, bool storeFlammableCargo, int cargoCapacity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Result.Failure<Warehouse>("Name is not setted !");
            }
            if(storeFlammableCargo == null)
            {
                return Result.Failure<Warehouse>("Store flammable cargo is not setted !");
            }
            if (cargoCapacity == null || cargoCapacity < 0)
            {
                return Result.Failure<Warehouse>("Cargo capacity is not setted or negative !");
            }
            Result<Warehouse> result = new Warehouse(id, name, storeFlammableCargo, cargoCapacity);
            return result;
        }
    }
}
