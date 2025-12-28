using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;

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
                var getCitiesResult = _cityService.GettAllCities();

                if (getCitiesResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getCitiesResult);

                var cityDtos = getCitiesResult.Data
                    .OrderBy(c => c.Name)
                    .Select(c => new CityDto(c))
                    .ToList();

                return ResponseHelper.PrepareResponse(cityDtos);
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
