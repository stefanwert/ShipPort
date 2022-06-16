using Core.Model.Workers;
using Core.Service;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebShipPort.DTO;

namespace WebShipPort.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WarehouseClerkController : Controller
    {
        private readonly WarehouseClerkService WarehouseClerkService;

        public WarehouseClerkController(WarehouseClerkService warehouseClerkService)
        {
            WarehouseClerkService = warehouseClerkService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            IEnumerable<WarehouseClerk> warehouseList = WarehouseClerkService.GetAll();
            var retList = warehouseList.Select(x => new WarehouseClerkDTO(x));
            return Ok(warehouseList);
        }

        [HttpPost]
        public IActionResult Create(WarehouseClerkDTO warehouseClerkDTO)
        {
            var id = Guid.NewGuid();
            Result<WarehouseClerk> result = WarehouseClerk.Create(warehouseClerkDTO.ClerkRole,
                id, warehouseClerkDTO.Name, warehouseClerkDTO.Surname, warehouseClerkDTO.Age,
                warehouseClerkDTO.YearsOfWorking, warehouseClerkDTO.Salary, warehouseClerkDTO.IsAvailable, warehouseClerkDTO.ShipPortId);
            if (result.IsFailure)
                return BadRequest(result.Error);

            Result<WarehouseClerk> warehouseCreated = WarehouseClerkService.Create(result.Value);

            if (warehouseCreated.IsFailure)
                return BadRequest(warehouseCreated.Error);

            return Ok(new WarehouseClerkDTO(warehouseCreated.Value));
        }

        [HttpPut("update")]
        public IActionResult Update(WarehouseClerkDTO warehouseClerkDTO)
        {
            Result<WarehouseClerk> warehouse = WarehouseClerk.Create(warehouseClerkDTO.ClerkRole,
                warehouseClerkDTO.Id, warehouseClerkDTO.Name, warehouseClerkDTO.Surname, warehouseClerkDTO.Age,
                warehouseClerkDTO.YearsOfWorking, warehouseClerkDTO.Salary, warehouseClerkDTO.IsAvailable, warehouseClerkDTO.ShipPortId);
            if (warehouse.IsFailure)
                return BadRequest(warehouse.Error);

            Result<WarehouseClerk> warehouseUpdated = WarehouseClerkService.Update(warehouse.Value);
            if (warehouseUpdated.IsFailure)
                return BadRequest(warehouseUpdated.Error);

            return Ok(new WarehouseClerkDTO(warehouseUpdated.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Maybe<WarehouseClerk> warehouse = WarehouseClerkService.DeleteById(id);
            if (warehouse.HasNoValue)
                return BadRequest("There is no warehouse with id:" + id);
            return Ok(new WarehouseClerkDTO(warehouse.Value));
        }
    }
}
