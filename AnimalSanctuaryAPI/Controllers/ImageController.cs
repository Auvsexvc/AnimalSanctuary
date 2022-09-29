using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.Interfaces;
using AnimalSanctuaryAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;

        public ImageController(IImageService imageService)
        {
            _service = imageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageViewModel>>> GetImages()
        {
            var data = await _service.GetAllAsync();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImageViewModel>> GetImage(Guid id)
        {
            var data = await _service.GetByIdAsync(id);

            return data == null ? NotFound() : Ok(data);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> PostImage([FromRoute] Guid id)
        {
            var files = HttpContext.Request.Form.Files;

            if (files.Count == 0)
            {
                return NoContent();
            }

            foreach (var file in files)
            {
                await _service.Upload(file, id);
            }

            return Ok();
        }
    }
}