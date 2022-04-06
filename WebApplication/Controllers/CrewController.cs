using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model.Workers;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using WebShipPort.DTO;

namespace WebShipPort.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CrewController : Controller
    {
        private readonly CrewService CrewService;

        public CrewController(CrewService crewService)
        {
            CrewService = crewService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            List<Crew> ret = CrewService.GetAll().ToList();
            return Ok(ret);
        }

        //[HttpPost]
        //public IActionResult Create(CrewDTO crewDTO)
        //{
        //    var id = Guid.NewGuid();
        //    Result<Crew> result = Crew.Create(crewDTO.crew,
        //        id, crewDTO.Name, crewDTO.Surname, crewDTO.Age,
        //        crewDTO.YearsOfWorking, crewDTO.Salary, crewDTO.IsAvailable);
        //    if (result.IsFailure)
        //        return BadRequest(result.Error);

        //    Result<WarehouseClerk> warehouseCreated = WarehouseClerkService.Create(result.Value);

        //    if (warehouseCreated.IsFailure)
        //        return BadRequest(warehouseCreated.Error);

        //    return Ok(warehouseCreated.Value);
        //}

        //[HttpPut("update")]
        //public IActionResult Update(WarehouseClerkDTO warehouseClerkDTO)
        //{
        //    Result<WarehouseClerk> warehouse = WarehouseClerk.Create(warehouseClerkDTO.ClerkRole,
        //        warehouseClerkDTO.Id, warehouseClerkDTO.Name, warehouseClerkDTO.Surname, warehouseClerkDTO.Age,
        //        warehouseClerkDTO.YearsOfWorking, warehouseClerkDTO.Salary, warehouseClerkDTO.IsAvailable);
        //    if (warehouse.IsFailure)
        //        return BadRequest(warehouse.Error);

        //    Result<WarehouseClerk> warehouseUpdated = WarehouseClerkService.Update(warehouse.Value);
        //    if (warehouseUpdated.IsFailure)
        //        return BadRequest(warehouseUpdated.Error);

        //    return Ok(warehouseUpdated.Value);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteById(Guid id)
        //{
        //    Maybe<WarehouseClerk> warehouse = WarehouseClerkService.DeleteById(id);
        //    if (warehouse.HasNoValue)
        //        return BadRequest("There is no warehouse with id:" + id);
        //    return Ok(warehouse);
        //}
    }
}
