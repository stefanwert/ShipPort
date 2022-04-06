using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShipPort.Controllers
{
    public class ShipCaptainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
