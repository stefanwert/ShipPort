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
using WebShipPort.Factory;

namespace WebShipPort.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransportController : Controller
    {
        private readonly TransportService _transportService;
        private readonly TransportFactory _transportFactory;
        public TransportController(TransportService transportService, TransportFactory transportFactory)
        {
            _transportService = transportService;
            _transportFactory = transportFactory;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            List<Transport> ret = _transportService.GetAll().ToList();
            return Ok(ret);
        }

        [HttpPost]
        public IActionResult Create(TransportDTO transportDTO)
        {
            var id = Guid.NewGuid();
            transportDTO.Id = id;
            var transport = _transportFactory.Create(transportDTO);
            if (transport.IsFailure)
                return BadRequest(transport.Error);

            var createdTransport = _transportService.Create(transport.Value);
            if (createdTransport.IsFailure)
                return BadRequest(createdTransport.Error);
            return Ok(createdTransport.Value);
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
