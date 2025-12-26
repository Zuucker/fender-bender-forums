using Application.Dtos.ModelDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using Domain.Errors;
using Presentation.Responses;
using Application.Services;
using System.Linq;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IPostService _postService;

        public UserController(IUserService userService,
                              IAuthenticatorService authenticatorService,
                              IPostService postService)
        {
            _userService = userService;
            _authenticatorService = authenticatorService;
            _postService = postService;
        }

        [HttpGet("get/{userId?}")]
        public IActionResult GetUserData(string? userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;
                }

                if (string.IsNullOrEmpty(userId))
                    return ResponseHelper.PrepareResponse(ApiErrors.UserNotFound);



                var getResult = _userService.GetUserById(userId ?? string.Empty);

                if (getResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getResult);



                return ResponseHelper.PrepareResponse(new UserDto(getResult.Data));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOffers {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("edit")]
        public IActionResult EditUser([FromBody] UserDto editedUser)
        {
            try
            {
                var userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (userId != editedUser.Id)
                    return ResponseHelper.PrepareResponse(ApiErrors.CantAccessDifferentUser);

                if (editedUser.NewPassword != editedUser.ConfirmPassword)
                    return ResponseHelper.PrepareResponse(ApiErrors.PasswordsDontMatch);



                var userResult = _userService.GetUserById(editedUser.Id);

                if (userResult.HasFailed())
                    return ResponseHelper.PrepareResponse(userResult);



                var verificationResult = _authenticatorService
                    .VerifyPassword(editedUser.Password, userResult.Data.PasswordHash ?? string.Empty);

                if (verificationResult.HasFailed())
                    return ResponseHelper.PrepareResponse(verificationResult);



                var updateResult = _userService.UpdateUser(userResult.Data, editedUser);

                if (updateResult.HasFailed())
                    return ResponseHelper.PrepareResponse(updateResult);


                return ResponseHelper.PrepareResponse(new UserDto(updateResult.Data));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOffers {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userId}/posts")]
        public IActionResult GetUsersPosts([FromRoute] Guid userId)
        {
            try
            {
                var getUserResult = _userService.GetUserById(userId.ToString());

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getUserResult);


                var getPostsResult = _postService.GetUsersPosts(userId);

                if (getPostsResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getPostsResult);


                var posts = getPostsResult.Data;

                var postDtos = posts
                    .Select(p => new PostDto(p, getUserResult.Data));

                return ResponseHelper.PrepareResponse(postDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUserPosts {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
