using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

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
                var posts = _postService.GetAllPosts();

                return Ok(posts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPosts {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("add")]
        public IActionResult AddPost([FromBody] AddPostRequest requestDto)
        {
            try
            {
                _postService.AddPost(requestDto);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddPost {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
