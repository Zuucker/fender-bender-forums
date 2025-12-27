using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;

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
                var getResult = _carService.GetAllCars();

                if (getResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getResult);

                var carDtos = getResult.Data
                    .Select(c => new CarDto(c))
                    .ToList();

                return ResponseHelper.PrepareResponse(carDtos);
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