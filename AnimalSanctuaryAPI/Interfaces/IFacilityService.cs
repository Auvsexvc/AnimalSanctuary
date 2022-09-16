using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IFacilityService
    {
        Task<FacilityViewModel?> Add(FacilityDto dto);
        Task<Guid?> Delete(Guid id);
        Task<FacilityViewModel?> GetViewModel(Guid id);
        Task<IEnumerable<FacilityViewModel>?> GetViewModels(string? sortingField, string? sortingOrder, string? filteringString);
        Task<FacilityViewModel?> Update(Guid id, FacilityDto dto);
    }
}