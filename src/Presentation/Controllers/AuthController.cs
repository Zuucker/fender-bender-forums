using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;
using Domain.Errors;

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
                var validationResult = _authenticator.IsValidUser(request);

                if (validationResult.HasFailed())
                    return ResponseHelper.PrepareResponse(validationResult);



                var generateResult = _authenticator.GenerateToken(request.Login);

                if (generateResult.HasFailed())
                    return ResponseHelper.PrepareResponse(generateResult.Error);



                return ResponseHelper.PrepareResponse(generateResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (request.Password != request.ConfirmPassword)
                    return ResponseHelper.PrepareResponse(ApiErrors.PasswordsDontMatch);



                var registerResult = _userService.RegisterUser(request.Username, request.Email, request.Password);

                if (registerResult.HasFailed())
                    return ResponseHelper.PrepareResponse(registerResult.Error);



                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Register {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
