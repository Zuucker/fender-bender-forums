using Application.Common;
using Application.Dtos.ModelDtos;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/section")]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet("get-list")]
        public IActionResult GetSections()
        {
            try
            {
                var getResult = _sectionService.GetSections();

                if (getResult.HasFailed())
                    return ResponseHelper.PrepareResponse(getResult);



                return ResponseHelper.PrepareResponse(getResult.Data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetSections {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
