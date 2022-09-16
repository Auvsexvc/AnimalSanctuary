using WebApp.Data;
using WebApp.Dtos;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IAnimalsService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalDto dto);
        Task<HttpResponseMessage?> DeleteAsync(Guid? id);
        Task<HttpResponseMessage?> EditAsync(Guid id, AnimalDto dto);
        Task<IEnumerable<T>?> GetAllAsync<T>(string? sortingField, string? sortingOrder, string? filteringString);
        Task<Animal?> GetByIdAsync(Guid? id);
        Task<NewAnimalDropdownsVM> GetNewAnimalDropdownsVM();
    }
}