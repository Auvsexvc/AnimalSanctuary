using WebApp.Dtos;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IAnimalSpecieService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalSpecieDto dto);

        Task<HttpResponseMessage?> DeleteAsync(Guid id);

        Task<HttpResponseMessage?> EditAsync(Guid id, UpdateSpecieViewModel vm);

        Task<IEnumerable<AnimalSpecieViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalSpecieViewModel?> GetByIdAsync(Guid id);
        Task<UpdateSpecieViewModel?> GetByIdUpdateModelAsync(Guid id);

        SortingDropdowns GetSortingDropdownsVM();

        Task<NewSpecieDropdownsVM> GetNewSpecieDropdownsVM();
    }
}