using WebApp.Data;
using WebApp.Dtos;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Services
{
    public class AnimalSpecieService : IAnimalSpecieService
    {
        private readonly IBaseService _baseService;

        public AnimalSpecieService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<HttpResponseMessage?> CreateAsync(AnimalSpecieDto dto)
        {
            return await _baseService.CreateAsync<AnimalSpecieDto>(dto);
        }
        public async Task<HttpResponseMessage?> DeleteAsync(Guid id)
        {
            return await _baseService.DeleteAsync<AnimalSpecie>(id);
        }
        public async Task<HttpResponseMessage?> EditAsync(Guid id, AnimalSpecieDto dto)
        {
            return await _baseService.EditAsync<AnimalSpecieDto>(id, dto);
        }
        public async Task<IEnumerable<AnimalSpecie>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            return await _baseService.GetAllAsync<AnimalSpecie>(sortingField, sortingOrder, filteringString);
        }
        public async Task<AnimalSpecie?> GetByIdAsync(Guid id)
        {
            return await _baseService.GetByIdAsync<AnimalSpecie>(id);
        }

        public async Task<NewSpecieDropdownsVM> GetNewAnimalDropdownsVM()
        {
            return new NewSpecieDropdownsVM()
            {
                Types = (await _baseService.GetAllAsync<AnimalType>(null, null, null))?.OrderBy(a => a.Name).ToList(),
            };
        }
    }
}
