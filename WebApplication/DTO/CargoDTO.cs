using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class CargoDTO
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public bool Flammable { get; private set; }

        public CargoDTO(Cargo cargo)
        {
            Id = cargo.Id;
            Name = cargo.Name;
            Quantity = cargo.Quantity;
            Flammable = cargo.Flammable;
        }
    }
}
