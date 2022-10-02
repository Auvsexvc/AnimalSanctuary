using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalSanctuaryAPI.Controllers
{
    [Authorize(Roles = "Administrator, Manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly IFacilityService _service;

        public FacilityController(IFacilityService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll(string? sortingField, string? sortingOrder, string? filteringString)
        {
            var data = await _service.GetAllAsync(sortingField, sortingOrder, filteringString);

            return data == null ? NotFound() : Ok(data);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _service.GetByIdAsync(id);

            return data == null ? NotFound() : Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, FacilityDto dto)
        {
            var data = await _service.UpdateAsync(id, dto);

            return data == null ? BadRequest() : Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(FacilityDto dto)
        {
            var data = await _service.AddAsync(dto);

            return data == null ? BadRequest() : CreatedAtAction("Get", new { id = data.Id }, data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedId = await _service.DeleteAsync(id);

            return deletedId == null ? NotFound() : Ok(deletedId);
        }
    }
}