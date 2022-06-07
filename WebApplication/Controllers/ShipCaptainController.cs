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
    public class ShipCaptainController : Controller
    {
        private readonly ShipCaptainService ShipCaptainService;

        public ShipCaptainController(ShipCaptainService shipCaptainService)
        {
            ShipCaptainService = shipCaptainService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            IEnumerable<ShipCaptain> listShipCap = ShipCaptainService.GetAll();
            var retList = listShipCap.Select(x => new ShipCaptainDTO(x));
            return Ok(retList);
        }


        [HttpPost]
        public IActionResult Create(ShipCaptainDTO shipCaptainDTO)
        {
            var id = Guid.NewGuid();
            Result<ShipCaptain> result = ShipCaptain.Create(shipCaptainDTO.SailingHoursTotal, shipCaptainDTO.SailingHoursAsCaptain, id, shipCaptainDTO.Name,
                shipCaptainDTO.Surname, shipCaptainDTO.Age, shipCaptainDTO.YearsOfWorking, shipCaptainDTO.Salary, shipCaptainDTO.IsAvailable);
            if (result.IsFailure)
                return BadRequest(result.Error);

            Result<ShipCaptain> shipCaptainCreated = ShipCaptainService.Create(result.Value);

            if (shipCaptainCreated.IsFailure)
                return BadRequest(shipCaptainCreated.Error);

            return Ok(new ShipCaptainDTO( shipCaptainCreated.Value));
        }

        [HttpPut("update")]
        public IActionResult Update(ShipCaptainDTO shipCaptainDTO)
        {
            Result<ShipCaptain> shipCaptain = ShipCaptain.Create(shipCaptainDTO.SailingHoursTotal, shipCaptainDTO.SailingHoursAsCaptain, shipCaptainDTO.Id, shipCaptainDTO.Name,
                shipCaptainDTO.Surname, shipCaptainDTO.Age, shipCaptainDTO.YearsOfWorking, shipCaptainDTO.Salary, shipCaptainDTO.IsAvailable);
            if (shipCaptain.IsFailure)
                return BadRequest(shipCaptain.Error);

            Result<ShipCaptain> shipCaptainUpdated = ShipCaptainService.Update(shipCaptain.Value);
            if (shipCaptainUpdated.IsFailure)
                return BadRequest(shipCaptainUpdated.Error);

            return Ok(new ShipCaptainDTO( shipCaptainUpdated.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Maybe<ShipCaptain> shipCaptain = ShipCaptainService.DeleteById(id);
            if (shipCaptain.HasNoValue)
                return BadRequest("There is no ship captain with id:" + id);
            return Ok(new ShipCaptainDTO(shipCaptain.Value));
        }

        [HttpGet("getAllByShipPortId/{shipCaptainId}")]
        public IActionResult getAllByShipPortId(Guid shipCaptainId)
        {
            if (shipCaptainId == Guid.Empty)
                return BadRequest("Ship port id is not setted");

            ICollection<ShipCaptain> ret = ShipCaptainService.FindByShipPortId(shipCaptainId);
            var retList = ret.Select(x => new ShipCaptainDTO(x));
            return Ok(retList);
        }
    }
}
