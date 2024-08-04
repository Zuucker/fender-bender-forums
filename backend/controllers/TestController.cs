using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/testController")]
    public class TestController : ControllerBase
    {
        [HttpGet("getString")]
        public IActionResult GetString()
        {
            try
            {
                var result = new { status = "works", data = "halo" };
                Console.WriteLine(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}
