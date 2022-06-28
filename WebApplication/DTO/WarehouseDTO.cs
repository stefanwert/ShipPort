using Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.DTO
{
    public class WarehouseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool StoreFlammableCargo { get; set; }

        public int CargoCapacity { get; set; }

        public Guid ShipPortId { get; set; }
        public ICollection<CargoDTO> CargoDTOs {get;set;}
        public WarehouseDTO() { }

        public WarehouseDTO(Warehouse warehouse)
        {
            Id = warehouse.Id;
            Name = warehouse.Name;
            StoreFlammableCargo = warehouse.StoreFlammableCargo;
            CargoCapacity = warehouse.CargoCapacity;
            ShipPortId = warehouse.ShipPortId;
            if(warehouse.Cargos!= null)
                CargoDTOs = warehouse.Cargos.Select(x => new CargoDTO(x)).ToList();
        }

    }
}
