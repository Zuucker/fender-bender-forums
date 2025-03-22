using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet("get")]
        public IActionResult GetCities()
        {
            try
            {
                var cities = _cityService.GettAllCities();

                return Ok(cities);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCities {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddCity([FromBody] AddCityRequest request)
        {
            try
            {
                var newCity = _cityService.AddCity(request);

                return Ok(newCity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddCity {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
