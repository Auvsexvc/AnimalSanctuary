using WebApp.Dtos;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IAnimalTypeService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalTypeDto dto);

        Task<HttpResponseMessage?> DeleteAsync(Guid id);

        Task<HttpResponseMessage?> EditAsync(Guid id, AnimalTypeViewModel vm);

        Task<IEnumerable<AnimalTypeViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalTypeViewModel?> GetByIdAsync(Guid id);

        SortingDropdowns GetSortingDropdownsVM();
    }
}