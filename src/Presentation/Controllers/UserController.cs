using Application.Dtos.RequestDtos;
using Application.Dtos.ModelDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Application.Common;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get")]
        [Authorize]
        public IActionResult GetUserData()
        {
            try
            {
                var userId = User?.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();
                

                var user = _userService.GetUserById(userId);

                if (user is null)
                    return NotFound("user not found");


                return Ok(new UserDto(user));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOffers {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("edit")]
        public IActionResult EditUser([FromBody] EditUserRequest request)
        {
            try
            {
                var updatedUser = _userService.UpdateUser(request.UserId, request.Username, request.Email)
                    ?? throw new Exception("User cannot be updated");

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOffers {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
