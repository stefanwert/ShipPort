using Core.Model;
using Core.Model.TransportStates;
using Core.Service;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShipPort.DTO;

namespace WebShipPort.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransportController : Controller
    {
        private readonly TransportService TransportService;
        public TransportController(TransportService transportService)
        {
            TransportService = transportService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            List<Transport> ret = TransportService.GetAll().ToList();
            return Ok(ret);
        }

        [HttpPost]
        public IActionResult Create(TransportDTO transportDTO)
        {
            var id = Guid.NewGuid();
            var transportState = CreatingTransport.Name;
            Result<Transport> result = Transport.Create(id, transportDTO.TimeFrom, transportDTO.TimeTo,
                transportDTO.Ship, transportDTO.ShipCaptains, transportDTO.Crew, transportDTO.ShipPortFrom,
                transportDTO.ShipPortTo, transportState, transportDTO.CurrentShipCaptain);
            if (result.IsFailure)
                return BadRequest(result.Error);

            Result<Transport> transport = TransportService.Create(result.Value);

            if (transport.IsFailure)
                return BadRequest(transport.Error);

            return Ok(transport.Value);
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
