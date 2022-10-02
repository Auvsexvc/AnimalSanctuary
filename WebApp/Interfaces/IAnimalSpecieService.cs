using WebClientApp.Dtos;
using WebClientApp.ViewModels;

namespace WebClientApp.Interfaces
{
    public interface IAnimalSpecieService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalSpecieDto dto, string accessToken);

        Task<HttpResponseMessage?> DeleteAsync(Guid id, string accessToken);

        Task<HttpResponseMessage?> EditAsync(Guid id, UpdateSpecieViewModel vm, string accessToken);

        Task<IEnumerable<AnimalSpecieViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalSpecieViewModel?> GetByIdAsync(Guid id);

        Task<UpdateSpecieViewModel?> GetByIdUpdateModelAsync(Guid id);

        SortingDropdownsViewModel GetSortingDropdownsVM();

        Task<NewSpecieDropdownsVM> GetNewSpecieDropdownsVMAsync();
    }
}