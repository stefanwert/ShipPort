using Core.Model;
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
    public class CargoController : Controller
    {
        private readonly CargoService CargoService;

        public CargoController(CargoService cargoService)
        {
            CargoService = cargoService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            IEnumerable<Cargo> crewList = CargoService.GetAll();
            var retList = crewList.Select(x => new CargoDTO(x));
            return Ok(retList);
        }

        [HttpGet("GetAllThatIsNotTrasnporting")]
        public IActionResult GetAllThatIsNotTrasnporting()
        {
            IEnumerable<Cargo> crewList = CargoService.GetAllThatIsNotTrasnporting();
            var retList = crewList.Select(x => new CargoDTO(x));
            return Ok(retList);
        }

        [HttpPost]
        public IActionResult Create(CargoDTO cargoDTO)
        {
            var id = Guid.NewGuid();
            Result<Cargo> result = Cargo.Create(cargoDTO.Id, cargoDTO.Name, cargoDTO.Quantity, cargoDTO.Flammable);
            if (result.IsFailure)
                return BadRequest(result.Error);

            Result<Cargo> cargoCreated = CargoService.Create(result.Value);

            if (cargoCreated.IsFailure)
                return BadRequest(cargoCreated.Error);

            return Ok(new CargoDTO(cargoCreated.Value));
        }

        [HttpPut("update")]
        public IActionResult Update(CargoDTO cargoDTO)
        {
            Result<Cargo> result = Cargo.Create(cargoDTO.Id, cargoDTO.Name, cargoDTO.Quantity, cargoDTO.Flammable);
            if (result.IsFailure)
                return BadRequest(result.Error);

            Result<Cargo> cargoUpdated = CargoService.Update(result.Value);
            if (cargoUpdated.IsFailure)
                return BadRequest(cargoUpdated.Error);

            return Ok(new CargoDTO(cargoUpdated.Value));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(Guid id)
        {
            Maybe<Cargo> cargo = CargoService.DeleteById(id);
            if (cargo.HasNoValue)
                return BadRequest("There is no warehouse with id:" + id);
            return Ok(new CargoDTO((cargo.Value)));
        }

    }
}
