using Backend.ApiModels.Requests;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
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
