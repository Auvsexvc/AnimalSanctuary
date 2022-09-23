using WebApp.Dtos;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface ITypeService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalTypeDto dto, string accessToken);

        Task<HttpResponseMessage?> DeleteAsync(Guid id, string accessToken);

        Task<HttpResponseMessage?> EditAsync(Guid id, AnimalTypeViewModel vm, string accessToken);

        Task<IEnumerable<AnimalTypeViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalTypeViewModel?> GetByIdAsync(Guid id);

        SortingDropdowns GetSortingDropdownsVM();
    }
}