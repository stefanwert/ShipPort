using Core.Model.Users;
using Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using WebShipPort.DTO;

namespace WebShipPort.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserService UserService;

        public UserController(UserService userService)
        {
            UserService = userService;
        }

        [HttpGet(nameof(UserRole.Admin))]
        [Authorize]
        public IActionResult Index()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Name} your current role is {currentUser.Role}");
        }

        [HttpGet("Sellers")]
        [Authorize(Roles = "Seller")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Name}, you are a {currentUser.Role}");
        }

        [HttpGet("AdminsAndSellers")]
        [Authorize(Roles = "Administrator,Seller")]
        public IActionResult AdminsAndSellersEndpoint()
        {
            var currentUser = GetCurrentUser();

            return Ok($"Hi {currentUser.Name}, you are an {currentUser.Role}");
        }

        [HttpGet("Public")]
        [Authorize]
        public IActionResult Public()
        {
            return Ok("Hi, you're on public property");
        }

        [HttpPost("Register")]
        public IActionResult Register(UserDTO user)
        {
            var ret = Core.Model.Users.User.Create(new Guid(), user.Email, user.Password, user.Name, user.Surename, user.Role);
            if (ret.IsFailure)
            {
                return BadRequest(ret.Error);
            }
            var userCreated = UserService.Create(ret.Value);
            if (userCreated.IsFailure)
            {
                return BadRequest(userCreated.Error);
            }
            return Ok(userCreated);
        }
        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                return new User
                {
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    Name = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                    Role = (UserRole)Enum.Parse(typeof(UserRole), userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value),
                    Surename = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                };
            }
            return null;
        }
    }
}
