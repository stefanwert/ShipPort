using Core.Model;
using Core.Service;
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
    public class ShipPortController : Controller
    {
        private readonly ShipPortService ShipPortService;

        public ShipPortController(ShipPortService shipPortService)
        {
            ShipPortService = shipPortService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            List<ShipPort> ret = ShipPortService.GetAll().ToList();
            return Ok(ret);
        }

        //[HttpPost]
        //public IActionResult Create(ShipPortDTO shipPortDTO)
        //{
        //    var id = Guid.NewGuid();
        //    Result<WarehouseClerk> result = WarehouseClerk.Create(warehouseClerkDTO.ClerkRole,
        //        id, warehouseClerkDTO.Name, warehouseClerkDTO.Surname, warehouseClerkDTO.Age,
        //        warehouseClerkDTO.YearsOfWorking, warehouseClerkDTO.Salary, warehouseClerkDTO.IsAvailable);
        //    if (result.IsFailure)
        //        return BadRequest(result.Error);

        //    Result<WarehouseClerk> warehouseCreated = WarehouseClerkService.Create(result.Value);

        //    if (warehouseCreated.IsFailure)
        //        return BadRequest(warehouseCreated.Error);

        //    return Ok(warehouseCreated.Value);
        //}
    }
}
