using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost("edit")]
        public IActionResult AddCity([FromBody] EditUserRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserId))
                    throw new Exception("UserId is required");

                var updatedUser = _userService.UpdateUser(request.UserId, request.Username, request.Email)
                    ?? throw new Exception("User cannot be updated");

                return Ok(new { updatedUser });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
