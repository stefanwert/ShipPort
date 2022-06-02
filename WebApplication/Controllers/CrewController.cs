﻿using Core.Model.Workers;
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
            IEnumerable<Crew> crewList = CrewService.GetAll();
            var retList = crewList.Select(x => new CrewDTO(x));
            return Ok(retList);
        }

        [HttpPost]
        public IActionResult Create(CrewDTO crewDTO)
        {
            var id = Guid.NewGuid();
            Result<Crew> result = Crew.Create(crewDTO.SailingHoursTotal, crewDTO.Role, id, crewDTO.Name,
                crewDTO.Surname, crewDTO.Age, crewDTO.YearsOfWorking, crewDTO.Salary, crewDTO.IsAvailable);
            if (result.IsFailure)
                return BadRequest(result.Error);

            Result<Crew> crewCreated = CrewService.Create(result.Value);

            if (crewCreated.IsFailure)
                return BadRequest(crewCreated.Error);

            return Ok(new CrewDTO(crewCreated.Value));
        }

        [HttpPut("update")]
        public IActionResult Update(CrewDTO crewDTO)
        {
            Result<Crew> crew = Crew.Create(crewDTO.SailingHoursTotal, crewDTO.Role, crewDTO.Id, crewDTO.Name,
                crewDTO.Surname, crewDTO.Age, crewDTO.YearsOfWorking, crewDTO.Salary, crewDTO.IsAvailable);
            if (crew.IsFailure)
                return BadRequest(crew.Error);

            Result<Crew> crewUpdated = CrewService.Update(crew.Value);
            if (crewUpdated.IsFailure)
                return BadRequest(crewUpdated.Error);

            return Ok(new CrewDTO(crewUpdated.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Maybe<Crew> crew = CrewService.DeleteById(id);
            if (crew.HasNoValue)
                return BadRequest("There is no warehouse with id:" + id);
            return Ok(new CrewDTO((crew.Value)));
        }
    }
}
