using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticatorService _authenticator;

        public AuthController(IAuthenticatorService authenticator, IUserService userService)
        {
            _userService = userService;
            _authenticator = authenticator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                if (_authenticator.IsValidUser(request))
                {
                    var token = _authenticator.GenerateToken(request.Login);
                    return Ok(token);
                }

                return BadRequest("Invalid credentials");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (request.Password != request.ConfirmPassword)
                    throw new Exception("Password and ConfirmPassword don't match");

                var user = _userService.RegisterUser(request.Username, request.Email, request.Password);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
