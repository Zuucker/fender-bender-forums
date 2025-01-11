using Backend.ApiModels.Requests;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("add")]
        public IActionResult AddPost([FromBody] AddPostRequest requestDto)
        {
            try
            {
                if (requestDto == null)
                    throw new Exception("The request is empty of null");

                _postService.AddPost(requestDto);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
