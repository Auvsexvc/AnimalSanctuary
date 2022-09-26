using WebApp.Dtos;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IAnimalService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalDto dto, string accessToken);

        Task<HttpResponseMessage?> DeleteAsync(Guid id, string accessToken);

        Task<HttpResponseMessage?> EditAsync(Guid id, UpdateAnimalViewModel vm, string accessToken);

        Task<IEnumerable<AnimalViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalViewModel?> GetByIdAsync(Guid id);

        Task<UpdateAnimalViewModel?> GetByIdUpdateModelAsync(Guid id);

        Task<NewAnimalDropdownsVM> GetNewUserDropdownsVM();

        SortingDropdownsViewModel GetSortingDropdownsVM();
    }
}