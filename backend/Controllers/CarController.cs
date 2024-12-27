using Backend.ApiModels.Dtos;
using Backend.ApiModels.Requests;
using Backend.Services;
using Data;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;
        private readonly Authenticator _authenticator;

        public CarController(Authenticator authenticator, CarService carService)
        {
            _carService = carService;
            _authenticator = authenticator;
        }

        [HttpGet("get")]
        public IActionResult GetCars()
        {
            try
            {
                var cars = _carService.GetAllCars();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("add")]
        public IActionResult AddCar([FromBody] AddCarRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Model))
                    throw new Exception("Model is required");

                if (string.IsNullOrEmpty(request.Manufacturer))
                    throw new Exception("Manufacturer is required");

                var car = _carService.AddCar(request);

                return Ok(new { car });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}