using Application.Dtos.ModelDtos;
using Application.Interfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using Presentation.Responses;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/content")]
    public class MediaController : ControllerBase
    {
        private readonly IFileStorage _fileStorage;

        public MediaController(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        [HttpGet("{fileId:guid}.{extension}")]
        public IActionResult GetFile([FromRoute] Guid fileId, [FromRoute] string extension)
        {
            try
            {
                byte[] fileInBytes = _fileStorage.ReadImageFile(fileId);


                if (fileInBytes.Length == 0)
                    return ResponseHelper.PrepareResponse(ApiErrors.FileNotFound);


                return File(fileInBytes, $"image/{extension}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPosts {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
