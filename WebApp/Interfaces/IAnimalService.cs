using WebApp.Dtos;
using WebApp.Models;

namespace WebApp.Interfaces
{
    public interface IAnimalService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalDto dto);

        Task<HttpResponseMessage?> DeleteAsync(Guid id);

        Task<HttpResponseMessage?> EditAsync(Guid id, AnimalViewModel vm);

        Task<IEnumerable<AnimalSortingFieldsViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalSortingFieldsViewModel?> GetByIdAsync(Guid id);

        Task<AnimalViewModel?> GetByIdUpdateModelAsync(Guid id);

        Task<NewAnimalDropdownsVM> GetNewAnimalDropdownsVM();

        SortingDropdowns GetSortingDropdownsVM();
    }
}