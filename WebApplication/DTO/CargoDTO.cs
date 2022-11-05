using Core.Model;
using System;

namespace WebShipPort.DTO
{
    public class CargoDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool Flammable { get; set; }
        public string Image { get; set; }
        public Guid? TransportId { get; set; }
        public Guid? WarehouseId { get; set; }
        public CargoDTO() { }
        public CargoDTO(Cargo cargo)
        {
            Id = cargo.Id;
            Name = cargo.Name;
            Quantity = cargo.Quantity;
            Flammable = cargo.Flammable;
            Image = cargo.Image;
            TransportId = cargo.TransportId;
            WarehouseId = cargo.WarehouseId;
        }

    }
}
