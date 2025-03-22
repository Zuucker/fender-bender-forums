using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet("get")]
        public IActionResult GetOffers()
        {
            try
            {
                var offers = _offerService.GetAllOffers();

                return Ok(offers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetOffers {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("add")]
        public IActionResult AddOffer([FromBody] AddOfferRequest requestDto)
        {
            try
            {
                _offerService.AddOffer(requestDto);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddOffer {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("rating/add")]
        public IActionResult AddOfferRating([FromBody] AddOfferRatingRequest requestDto)
        {
            try
            {
                _offerService.AddOfferRating(requestDto);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddOfferRating {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
