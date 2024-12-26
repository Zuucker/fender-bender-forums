using backend.apiModels;
using Data;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/testController")]
    public class AuthController : ControllerBase
    {
        private readonly DataRepository _dataRepository;
        private readonly IConfiguration _configuration;

        public AuthController(DataRepository repository, IConfiguration configuration)
        {
            _dataRepository = repository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Login))
                return BadRequest("Login is required");

            if (string.IsNullOrEmpty(request.PasswordHash))
                return BadRequest("Password is required");

            Authenticator authenticator = new(_dataRepository, _configuration);

            if (await authenticator.IsValidUser(request))
            {
                var token = authenticator.GenerateToken(request.Login);
                return Ok(new { token });
            }

            return BadRequest("Invalid credentials");
        }
    }
}
