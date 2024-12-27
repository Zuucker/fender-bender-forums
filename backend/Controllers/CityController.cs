using Backend.ApiModels.Dtos;
using Backend.ApiModels.Requests;
using Backend.Services;
using Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Models;
using Utilities;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/city")]
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;
        private readonly Authenticator _authenticator;

        public CityController(Authenticator authenticator, CityService cityService)
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
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
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
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}
