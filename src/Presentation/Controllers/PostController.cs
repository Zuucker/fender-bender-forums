using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpGet("get")]
        public IActionResult GetPosts()
        {
            try
            {
                var getResult = _postService.GetAllPosts();


                if (getResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getResult);

                var postsDtos = getResult.Data
                    .Select(p => new PostDto(p))
                    .ToList();


                return ResponseHelper.PrepareResponse(postsDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPosts {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{userId}")]
        public IActionResult GetPosts([FromRoute] Guid userId)
        {
            try
            {
                var posts = _postService.GetUsersPosts(userId);


                return Ok(posts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUserPosts {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddPost([FromBody] AddPostRequest requestDto)
        {
            try
            {

                var userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return ResponseHelper.PrepareResponse(ApiErrors.UserNotFound);


                var addResult = _postService.AddPost(requestDto);

                if (addResult.HasFailed())
                    return ResponseHelper.PrepareResponse(addResult);



                return ResponseHelper.PrepareResponse(new PostDto(addResult.Data));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddPost {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
