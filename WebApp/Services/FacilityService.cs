using WebApp.Dtos;
using WebApp.Data;
using WebApp.Interfaces;

namespace WebApp.Services
{
    public class FacilityService : IFacilityService
    {
        private readonly IBaseService _baseService;

        public FacilityService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<HttpResponseMessage?> CreateAsync(FacilityDto dto)
        {
            return await _baseService.CreateAsync<FacilityDto>(dto);
        }
        public async Task<HttpResponseMessage?> DeleteAsync(Guid id)
        {
            return await _baseService.DeleteAsync<Facility>(id);
        }
        public async Task<HttpResponseMessage?> EditAsync(Guid id, FacilityDto dto)
        {
            return await _baseService.EditAsync<FacilityDto>(id, dto);
        }
        public async Task<IEnumerable<Facility>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString)
        {
            return await _baseService.GetAllAsync<Facility>(sortingField, sortingOrder, filteringString);
        }
        public async Task<Facility?> GetByIdAsync(Guid id)
        {
            return await _baseService.GetByIdAsync<Facility>(id);
        }
    }
}
