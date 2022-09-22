using WebApp.Dtos;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IFacilityService
    {
        Task<HttpResponseMessage?> CreateAsync(FacilityDto dto, string accessToken);

        Task<HttpResponseMessage?> DeleteAsync(Guid id, string accessToken);

        Task<HttpResponseMessage?> EditAsync(Guid id, UpdateFacilityViewModel vm, string accessToken);

        Task<IEnumerable<FacilityViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<FacilityViewModel?> GetByIdAsync(Guid id);

        Task<UpdateFacilityViewModel?> GetByIdUpdateModelAsync(Guid id);

        Task<NewFacilityDropdownsVM> GetNewFacilityDropdownsVM();

        SortingDropdowns GetSortingDropdownsVM();
    }
}