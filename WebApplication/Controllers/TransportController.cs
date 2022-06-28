using Core.Model;
using Core.Model.TransportStates;
using Core.Service;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            IEnumerable<Transport> transportList = _transportService.GetAll();
            var retList = transportList.Select(x => new TransportDTO(x));
            return Ok(transportList);
        }

        [HttpPost]
        public IActionResult Create(TransportDTO transportDTO)
        {
            var id = Guid.NewGuid();
            transportDTO.Id = id;
            transportDTO.TransportState = CreatingTransport.Name;
            var transport = _transportFactory.Create(transportDTO);
            if (transport.IsFailure)
                return BadRequest(transport.Error);

            var createdTransport = _transportService.Create(transport.Value);
            if (createdTransport.IsFailure)
                return BadRequest(createdTransport.Error);
            return Ok(new TransportDTO(createdTransport.Value));
        }

        [HttpPut("update")]
        public IActionResult Update(TransportDTO transportDTO)
        {
            var transport = _transportFactory.Create(transportDTO);
            if (transport.IsFailure)
                return BadRequest(transport.Error);

            var updatedTransport = _transportService.Update(transport.Value);
            if (updatedTransport.IsFailure)
                return BadRequest(updatedTransport.Error);

            return Ok(new TransportDTO(updatedTransport.Value));
        }

        [HttpPut("cancel/{id}")]
        public IActionResult CancelTransport(Guid id)
        {
            var transport = _transportService.FindById(id);
            if (transport.HasNoValue)
                return BadRequest("There is no transport with that id");

            if (!transport.Value.CancelTransport())
                return BadRequest("You can only cancel transport that have status CREATING");
            var retTransport = _transportService.Update(transport.Value);

            return Ok(new TransportDTO(retTransport.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Maybe<Transport> transport = _transportService.DeleteById(id);
            if (transport.HasNoValue)
                return BadRequest("There is no transport with id:" + id);
            return Ok(new TransportDTO(transport.Value));
        }

        [HttpGet("getAllByShipPortId/{id}")]
        public IActionResult FindByShipPortId(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Ship port id is not setted");

            ICollection<Transport> ret = _transportService.FindByShipPortId(id);
            var retList = ret.Select(x => new TransportDTO(x));
            return Ok(retList);
        }
    } 
}
