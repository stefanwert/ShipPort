using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;
using Core.Service;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using WebShipPort.DTO;

namespace WebShipPort.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WarehouseController : Controller
    {
        private readonly WarehouseService WarehouseService;

        public WarehouseController(WarehouseService warehouseService)
        {
            WarehouseService = warehouseService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Warehouse> warehouseList = WarehouseService.GetAll();
            var retList = warehouseList.Select(x => new WarehouseDTO(x));
            return Ok(retList);
        }

        [HttpPost]
        public IActionResult Create(WarehouseDTO warehouseDTO)
        {
            var id = Guid.NewGuid();
            Result<Warehouse> result = Warehouse.Create(id, warehouseDTO.Name, warehouseDTO.StoreFlammableCargo, warehouseDTO.CargoCapacity);
            if (result.IsFailure)
                return BadRequest(result.Error);

            Result<Warehouse> warehouseCreated = WarehouseService.Create(result.Value);

            if (warehouseCreated.IsFailure)
                return BadRequest(warehouseCreated.Error);

            return Ok(new WarehouseDTO(warehouseCreated.Value));
        }

        [HttpPut("update")]
        public IActionResult Update(WarehouseDTO warehouseDTO)
        {
            Result<Warehouse> warehouse = Warehouse.Create(warehouseDTO.Id, warehouseDTO.Name, warehouseDTO.StoreFlammableCargo, warehouseDTO.CargoCapacity);
            if (warehouse.IsFailure)
                return BadRequest(warehouse.Error);

            Result<Warehouse> warehouseUpdated = WarehouseService.Update(warehouse.Value);
            if (warehouseUpdated.IsFailure)
                return BadRequest(warehouseUpdated.Error);

            return Ok(new WarehouseDTO(warehouseUpdated.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Maybe<Warehouse> warehouse = WarehouseService.DeleteById(id);
            if (warehouse.HasNoValue)
                return BadRequest("There is no warehouse with id:" + id);
            return Ok(new WarehouseDTO(warehouse.Value));
        }

        [HttpGet("getAllByShipPortId/{shipPortId}")]
        public IActionResult getAllByShipPortId(Guid shipPortId)
        {
            if (shipPortId == Guid.Empty)
                return BadRequest("Ship port id is not setted");

            IEnumerable<Warehouse> ret = WarehouseService.FindByShipPortId(shipPortId);
            var retList = ret.Select(x => new WarehouseDTO(x));
            return Ok(retList);
        }

    }
}
