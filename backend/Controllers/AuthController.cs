using Backend.ApiModels.Requests;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly Authenticator _authenticator;

        public AuthController(Authenticator authenticator, UserService userService)
        {
            _userService = userService;
            _authenticator = authenticator;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Login))
                    return BadRequest("Login is required");

                if (string.IsNullOrEmpty(request.PasswordHash))
                    return BadRequest("Password is required");

                if (_authenticator.IsValidUser(request))
                {
                    var token = _authenticator.GenerateToken(request.Login);
                    return Ok(new { token });
                }

                return BadRequest("Invalid credentials");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email))
                    throw new Exception("Email is required");

                if (string.IsNullOrEmpty(request.Username))
                    throw new Exception("Username is required");

                if (string.IsNullOrEmpty(request.Password))
                    throw new Exception("Password is required");

                if (string.IsNullOrEmpty(request.ConfirmPassword))
                    throw new Exception("ConfirmPassword is required");

                if (request.Password != request.ConfirmPassword)
                    throw new Exception("Password and ConfirmPassword don't match");


                var user = _userService.RegisterUser(request.Username, request.Email, request.Password);

                //no reply message needed, the user needs to login anyways
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
