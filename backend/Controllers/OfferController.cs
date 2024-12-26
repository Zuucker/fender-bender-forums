using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/offerController")]
    public class OfferController : ControllerBase
    {
        private readonly DataRepository _dataRepository;
        private readonly IConfiguration _configuration;

        public OfferController(DataRepository repository, IConfiguration configuration)
        {
            _dataRepository = repository;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("getOffers")]
        public IActionResult GetOffers()
        {
            try
            {
                var offers = _dataRepository.GetAll<Offer>();
                return Ok(offers);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}
