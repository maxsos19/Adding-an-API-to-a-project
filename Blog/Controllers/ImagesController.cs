using Blog.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly ILogger<ImagesController> _logger;
        public ImagesController(IImageRepository imageRepository, ILogger<ImagesController> logger)
        {
            this.imageRepository = imageRepository;
            _logger = logger;
            _logger.LogDebug(1, "NLog подключен к ImagesController");
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageURL = await imageRepository.UploadAsync(file);

            if (imageURL == null)
            {
                return Problem("Something went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }
            _logger.LogInformation("ImagesController - обращение к методу Upload");
            return new JsonResult(new { link = imageURL });
        }
    }
}
