using DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace HotelBookingSystemApi.Controllers
{
   
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var result = _service.Register(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var result = _service.Login(dto);
            return Ok(new { token = result });
        }
    }
}
