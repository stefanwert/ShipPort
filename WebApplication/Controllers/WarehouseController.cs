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
            List<Warehouse> ret = WarehouseService.GetAll().ToList();
            return Ok(ret);
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

            return Ok(warehouseCreated.Value);
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

            return Ok(warehouseUpdated.Value);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Maybe<Warehouse> warehouse = WarehouseService.DeleteById(id);
            if (warehouse.HasNoValue)
                return BadRequest("There is no warehouse with id:" + id);
            return Ok(warehouse);
        }

    }
}
