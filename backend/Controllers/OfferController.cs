using Backend.ApiModels.Requests;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OfferController : ControllerBase
    {
        private readonly OfferService _offerService;

        public OfferController(OfferService offerService)
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("add")]
        public IActionResult AddOffer([FromBody] AddOfferRequest requestDto)
        {
            try
            {
                if (requestDto == null)
                    throw new Exception("The request is empty of null");

                _offerService.AddOffer(requestDto);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("rating/add")]
        public IActionResult AddOfferRating([FromBody] AddOfferRatingRequest requestDto)
        {
            try
            {
                if (requestDto == null)
                    throw new Exception("The request is empty of null");

                _offerService.AddOfferRating(requestDto);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
