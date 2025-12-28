using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Dtos.RequestDtos;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IUserService _userService;

        public OfferController(IOfferService offerService,
            IUserService userService)
        {
            _offerService = offerService;
            _userService = userService;
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

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddOffer([FromBody] AddOfferRequest requestDto)
        {
            try
            {
                var userId = User.FindFirst(Constants.ClaimsConstants.UserIdClaim)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return ResponseHelper.PrepareResponse(ApiErrors.UserNotFound);

                var getUserResult = _userService.GetUserById(userId);

                if (getUserResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getUserResult);


                var addResult = _offerService.AddOffer(requestDto);

                if (addResult.HasFailed())
                    return ResponseHelper.PrepareResponse(addResult);



                return ResponseHelper.PrepareResponse(new OfferDto(addResult.Data, getUserResult.Data));
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
