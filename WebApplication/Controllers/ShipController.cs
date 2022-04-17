using Core.Model;
using Core.Service;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShipPort.DTO;
using WebShipPort.Factory;

namespace WebShipPort.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShipController : Controller
    {
        private readonly ShipService ShipService;

        private readonly ShipFactory ShipFactory;

        public ShipController(ShipService shipService, ShipFactory shipFactory)
        {
            ShipService = shipService;
            ShipFactory = shipFactory;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            List<Ship> ret = ShipService.GetAll().ToList();
            return Ok(ret);
        }
        [HttpPost]
        public IActionResult Create(ShipDTO shipDTO)
        {
            var id = Guid.NewGuid();
            var ship = ShipFactory.Create(shipDTO);
            if (ship.IsFailure)
                return BadRequest(ship.Error);

            Result<Ship> shipCreated = ShipService.Create(ship.Value);

            if (shipCreated.IsFailure)
                return BadRequest(shipCreated.Error);

            return Ok(shipCreated.Value);
        }

        //[HttpPut("update")]
        //public IActionResult Update(CrewDTO crewDTO)
        //{
        //    Result<Crew> crew = Crew.Create(crewDTO.SailingHoursTotal, crewDTO.Role, crewDTO.Id, crewDTO.Name,
        //        crewDTO.Surname, crewDTO.Age, crewDTO.YearsOfWorking, crewDTO.Salary, crewDTO.IsAvailable);
        //    if (crew.IsFailure)
        //        return BadRequest(crew.Error);

        //    Result<Crew> crewUpdated = CrewService.Update(crew.Value);
        //    if (crewUpdated.IsFailure)
        //        return BadRequest(crewUpdated.Error);

        //    return Ok(crewUpdated.Value);
        //}

        //[HttpDelete("{id}")]
        //public IActionResult DeleteById(Guid id)
        //{
        //    Maybe<Crew> crew = CrewService.DeleteById(id);
        //    if (crew.HasNoValue)
        //        return BadRequest("There is no warehouse with id:" + id);
        //    return Ok(crew);
        //}
    }
}
