using WebApp.Data;
using WebApp.Dtos;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IAnimalService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalDto dto);
        Task<HttpResponseMessage?> DeleteAsync(Guid id);
        Task<HttpResponseMessage?> EditAsync(Guid id, AnimalViewModel vm);
        Task<IEnumerable<AnimalViewModel?>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);
        Task<AnimalViewModel?> GetByIdAsync(Guid id);
        Task<NewAnimalDropdownsVM> GetNewAnimalDropdownsVM();
    }
}