using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/testController")]
    public class TestController : ControllerBase
    {
        private readonly DatabaseContext context;

        public TestController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet("getString")]
        public IActionResult GetString()
        {
            try
            {
                var result = new { status = "works", data = "halo" };
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("getCars")]
        public async Task<IActionResult> GetCars()
        {
            try
            {
                var cars = await context.Cars.ToListAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("getCities")]
        public async Task<IActionResult> GetCities()
        {
            try
            {
                var cities = await context.Cities.ToListAsync();
                return Ok(cities);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet("getOffers")]
        public async Task<IActionResult> GetOffers()
        {
            try
            {
                var offers = await context.Offers.ToListAsync();
                return Ok(offers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}
