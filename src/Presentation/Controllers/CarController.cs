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
        private readonly IAuthenticatorService _authenticator;

        public CarController(IAuthenticatorService authenticator, ICarService carService)
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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