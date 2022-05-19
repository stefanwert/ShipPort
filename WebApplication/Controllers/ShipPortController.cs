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
    public class ShipPortController : Controller
    {
        private readonly ShipPortService ShipPortService;
        private readonly ShipPortFactory ShipPortFactory;

        public ShipPortController(ShipPortService shipPortService, ShipPortFactory shipPortFactory)
        {
            ShipPortService = shipPortService;
            ShipPortFactory = shipPortFactory;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            List<ShipPort> ret = ShipPortService.GetAll().ToList();
            var retnew = ret.Select(x => new ShipPortDTO(x));
            return Ok(retnew);
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var shipPort = ShipPortService.FindById(id);
            if (shipPort.HasNoValue)
                return BadRequest("There is no ship port with id:" + id);

            var ret = new ShipPortDTO(shipPort.Value);
            return Ok(ret);
        }

        [HttpPost]
        public IActionResult Create(ShipPortDTO shipPortDTO)
        {
            var id = Guid.NewGuid();
            var shipPort = ShipPortFactory.Create(shipPortDTO);
            if (shipPort.IsFailure)
                return BadRequest(shipPort.Error);

            var createShipPort = ShipPortService.Create(shipPort.Value);
            if (createShipPort.IsFailure)
                return BadRequest(createShipPort.Error);

            return Ok(new ShipPortDTO(createShipPort.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Maybe<ShipPort> warehouse = ShipPortService.DeleteById(id);
            if (warehouse.HasNoValue)
                return BadRequest("There is no ship port with id:" + id);
            return Ok(warehouse);
        }

        [HttpPut("update")]
        public IActionResult Update(ShipPortDTO shipPortDTO)
        {
            Result<ShipPort> shipPort = ShipPortFactory.Create(shipPortDTO);
            if (shipPort.IsFailure)
                return BadRequest(shipPort.Error);

            Result<ShipPort> shipPortUpdated = ShipPortService.Update(shipPort.Value);
            if (shipPortUpdated.IsFailure)
                return BadRequest(shipPortUpdated.Error);

            return Ok(shipPortUpdated.Value);
        }
    }
}
