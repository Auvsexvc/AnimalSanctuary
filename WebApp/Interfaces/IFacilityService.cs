using AnimalSanctuaryAPI.Dtos;
using WebApp.Data;

namespace WebApp.Interfaces
{
    public interface IFacilityService
    {
        Task<HttpResponseMessage?> CreateAsync(FacilityDto dto);
        Task<HttpResponseMessage?> DeleteAsync(Guid id);
        Task<HttpResponseMessage?> EditAsync(Guid id, FacilityDto dto);
        Task<IEnumerable<Facility>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);
        Task<Facility?> GetByIdAsync(Guid id);
    }
}