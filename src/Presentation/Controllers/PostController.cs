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
        private readonly IUserService _userService;

        public PostController(IPostService postService,
            IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }


        [HttpGet("get")]
        public IActionResult GetPosts()
        {
            try
            {
                var userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return ResponseHelper.PrepareResponse(ApiErrors.UserNotFound);

                var getUserResult = _userService.GetUserById(userId);

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getUserResult);


                var getPostResult = _postService.GetAllPosts();


                if (getPostResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getPostResult);

                var postsDtos = getPostResult.Data
                    .Select(p => new PostDto(p, getUserResult.Data))
                    .ToList();


                return ResponseHelper.PrepareResponse(postsDtos);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPosts {ex.Message}");
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

                var getUserResult = _userService.GetUserById(userId);

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getUserResult);


                var addResult = _postService.AddPost(requestDto);

                if (addResult.HasFailed())
                    return ResponseHelper.PrepareResponse(addResult);



                return ResponseHelper.PrepareResponse(new PostDto(addResult.Data, getUserResult.Data));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddPost {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{postId}")]
        public IActionResult GetPost([FromRoute] int postId)
        {
            try
            {
                var userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return ResponseHelper.PrepareResponse(ApiErrors.UserNotFound);

                var getUserResult = _userService.GetUserById(userId);

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getUserResult);


                var getPostResult = _postService
                    .GetById(postId);

                if (getPostResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getPostResult);


                var postDto = new PostDto(getPostResult.Data, getUserResult.Data);


                foreach (var c in postDto.Contents)
                {
                    foreach (var ge in c.GalleryElements)
                    {
                        ge.Path = $"https://{Request.Host}/api/content/{ge.Path}";
                    }
                }

                return ResponseHelper.PrepareResponse(postDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPosts {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("comment/add")]
        public IActionResult AddPost([FromBody] AddCommentRequest requestDto)
        {
            try
            {
                var userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return ResponseHelper.PrepareResponse(ApiErrors.UserNotFound);

                var getUserResult = _userService.GetUserById(userId);

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getUserResult);


                var addResult = _postService.AddComment(requestDto);

                if (addResult.HasFailed())
                    return ResponseHelper.PrepareResponse(addResult);


                return ResponseHelper.PrepareResponse(new CommentDto(addResult.Data, getUserResult.Data));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddPost {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("interact")]
        public IActionResult InteractWithPost([FromBody] InteractWithPostRequest requestDto)
        {
            try
            {
                var userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return ResponseHelper.PrepareResponse(ApiErrors.UserNotFound);

                var getUserResult = _userService.GetUserById(userId);

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getUserResult);


                var interactionResult = _postService.InteractWithPost(requestDto, getUserResult.Data);

                if (interactionResult.HasFailed())
                    return ResponseHelper.PrepareResponse(interactionResult);


                var likeDto = interactionResult.Data != null 
                    ? new LikeDto(interactionResult.Data) 
                    : null;

                 return ResponseHelper.PrepareResponse(likeDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddPost {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("comment/interact")]
        public IActionResult InteractWithComment([FromBody] InteractWithCommentRequest requestDto)
        {
            try
            {
                var userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return ResponseHelper.PrepareResponse(ApiErrors.UserNotFound);

                var getUserResult = _userService.GetUserById(userId);

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getUserResult);


                var interactionResult = _postService.InteractWithComment(requestDto, getUserResult.Data);

                if (interactionResult.HasFailed())
                    return ResponseHelper.PrepareResponse(interactionResult);


                var likeDto = interactionResult.Data != null
                    ? new LikeDto(interactionResult.Data)
                    : null;

                return ResponseHelper.PrepareResponse(likeDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddPost {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
