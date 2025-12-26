using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/car")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
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
                Console.WriteLine($"GetCars {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add")]
        public IActionResult AddCar([FromBody] AddCarRequest request)
        {
            try
            {
                var car = _carService.AddCar(request);

                return Ok(car);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddCar {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}