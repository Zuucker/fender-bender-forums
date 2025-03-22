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
        private readonly IAuthenticatorService _authenticator;

        public CityController(IAuthenticatorService authenticator, ICityService cityService)
        {
            _cityService = cityService;
            _authenticator = authenticator;
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddCity([FromBody] AddCityRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                    throw new Exception("Name is required");

                var newCity = _cityService.AddCity(request);

                return Ok(new { newCity });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
