using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalSanctuaryAPI.Controllers
{
    [Authorize(Roles = "Administrator, Manager, User")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalSpecieController : ControllerBase
    {
        private readonly IAnimalSpecieService _service;

        public AnimalSpecieController(IAnimalSpecieService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(string? sortingField, string? sortingOrder, string? filteringString)
        {
            var data = await _service.GetViewModels(sortingField, sortingOrder, filteringString);

            return data == null ? NotFound() : Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _service.GetViewModel(id);

            return data == null ? NotFound() : Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, AnimalSpecieDto dto)
        {
            var data = await _service.Update(id, dto);

            return data == null ? BadRequest() : Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AnimalSpecieDto dto)
        {
            var data = await _service.Add(dto);

            return data == null ? BadRequest() : CreatedAtAction("Get", new { id = data.Id }, data);
        }

        [Authorize(Roles = "Administrator, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedId = await _service.Delete(id);

            return deletedId == null ? NotFound() : Ok(deletedId);
        }
    }
}