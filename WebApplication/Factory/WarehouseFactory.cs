using Core.Model;
using Core.Service;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShipPort.DTO;

namespace WebShipPort.Factory
{
    public class WarehouseFactory
    {

        private readonly WarehouseService _warehouseService;
        public WarehouseFactory(WarehouseService warehouseService) 
        {
            _warehouseService = warehouseService;
        }

        public Result<Warehouse> Create(WarehouseDTO warehouseDTO)
        {
            List<Cargo> cargos = new List<Cargo>();//= warehouseDTO.CargoDTOs.Select(x => Cargo.Create(x.Id, x.Name, x.Quantity, x.Flammable).Value).ToList();
            foreach(var cargo in warehouseDTO.CargoDTOs ?? Enumerable.Empty<CargoDTO>())
            {
                var cargoToAdd = Cargo.Create(cargo.Id, cargo.Name, cargo.Quantity, cargo.Flammable, cargo.Image);
                if (cargoToAdd.IsSuccess)
                    cargos.Add(cargoToAdd.Value);
            }
            Result<Warehouse> warehouseRet = Warehouse.Create(warehouseDTO.Id, warehouseDTO.Name, warehouseDTO.StoreFlammableCargo, warehouseDTO.CargoCapacity, warehouseDTO.ShipPortId, cargos);
            if (warehouseRet.IsFailure)
                return Result.Failure<Warehouse>(warehouseRet.Error);
            return warehouseRet;
        }
    }
}
