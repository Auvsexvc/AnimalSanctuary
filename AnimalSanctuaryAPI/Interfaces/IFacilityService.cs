using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IFacilityService
    {
        Task<FacilityViewModel?> AddAsync(FacilityDto dto);

        Task<Guid?> DeleteAsync(Guid id);

        Task<FacilityViewModel?> GetByIdAsync(Guid id);

        Task<IEnumerable<FacilityViewModel>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<FacilityViewModel?> UpdateAsync(Guid id, FacilityDto dto);
    }
}