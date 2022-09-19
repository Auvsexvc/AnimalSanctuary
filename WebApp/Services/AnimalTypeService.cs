using AnimalSanctuaryAPI.Dtos;
using WebApp.Data;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class AnimalTypeService
    {
        private readonly IBaseService _baseService;

        public AnimalTypeService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<HttpResponseMessage?> CreateAsync(AnimalTypeDto dto)
        {
            return await _baseService.CreateAsync<AnimalTypeDto>(dto);
        }
        public async Task<HttpResponseMessage?> DeleteAsync(Guid id)
        {
            return await _baseService.DeleteAsync<AnimalType>(id);
        }
        public async Task<HttpResponseMessage?> EditAsync(Guid id, AnimalTypeDto dto)
        {
            return await _baseService.EditAsync<AnimalTypeDto>(id, dto);
        }
        public async Task<IEnumerable<AnimalType>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            return await _baseService.GetAllAsync<AnimalType>(sortingField, sortingOrder, filteringString);
        }
        public async Task<AnimalType?> GetByIdAsync(Guid id)
        {
            return await _baseService.GetByIdAsync<AnimalType>(id);
        }
    }
}
