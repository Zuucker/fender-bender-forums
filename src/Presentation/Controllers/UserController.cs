using Application.Dtos.ModelDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Common;
using Domain.Errors;
using Presentation.Responses;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticatorService _authenticatorService;
        private readonly IOfferService _offerService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public UserController(IAuthenticatorService authenticatorService,
            IOfferService offerService,
            IPostService postService,
            IUserService userService)
        {
            _authenticatorService = authenticatorService;
            _offerService = offerService;
            _postService = postService;
            _userService = userService;
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
                Console.WriteLine($"GetUserData {ex.Message}");
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
                Console.WriteLine($"EditUser {ex.Message}");
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
                    return ResponseHelper.PrepareResponse(new List<PostDto>());


                var getPostsResult = _postService.GetUsersPosts(userId);

                if (getPostsResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getPostsResult);


                var postDtos = getPostsResult.Data
                    .Take(10)
                    .Select(p => new PostDto(p, getUserResult.Data))
                    .ToList();

                return ResponseHelper.PrepareResponse(postDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUsersPosts {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{userId}/offers")]
        public IActionResult GetUsersOffers([FromRoute] Guid userId)
        {
            try
            {
                var getUserResult = _userService.GetUserById(userId.ToString());

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(new List<PostDto>());


                var getOffersResult = _offerService.GetUsersOffers(userId);

                if (getOffersResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getOffersResult);


                var offersDtos = getOffersResult.Data
                    .OrderBy(o => o.Date)
                    .Take(10)
                    .Select(o => new OfferDto(o, getUserResult.Data))
                    .ToList();

                return ResponseHelper.PrepareResponse(offersDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUsersOffers {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
