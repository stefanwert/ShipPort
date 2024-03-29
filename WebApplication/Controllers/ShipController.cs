﻿using Core.Model;
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
            IEnumerable<Ship> shipList = ShipService.GetAll();
            var retList = shipList.Select(x => new ShipDTO(x));
            return Ok(retList);
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

            return Ok(new ShipDTO( shipCreated.Value));
        }

        [HttpPut("update")]
        public IActionResult Update(ShipDTO shipDTO)
        {
            var ship = ShipFactory.Create(shipDTO);
            if (ship.IsFailure)
                return BadRequest(ship.Error);

            var shipUpdated = ShipService.Update(ship.Value);
            if (shipUpdated.IsFailure)
                return BadRequest(shipUpdated.Error);

            return Ok(new ShipDTO(shipUpdated.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            var shipDeleted = ShipService.DeleteById(id);
            if (shipDeleted.HasNoValue)
                return BadRequest("There is no ship with id:" + id);
            return Ok(new ShipDTO(shipDeleted.Value));
        }

        [HttpGet("getAllByShipPortId/{id}")]
        public IActionResult getAllByShipPortId(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Ship port id is not setted");

            ICollection<Ship> ret = ShipService.FindByShipPortId(id);
            var retList = ret.Select(x => new ShipDTO(x));
            return Ok(retList);
        }
    }
}
