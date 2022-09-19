using AnimalSanctuaryAPI.Dtos;
using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IAnimalSpecieService
    {
        Task<HttpResponseMessage?> CreateAsync(AnimalSpecieDto dto);
        Task<HttpResponseMessage?> DeleteAsync(Guid id);
        Task<HttpResponseMessage?> EditAsync(Guid id, AnimalSpecieDto dto);
        Task<IEnumerable<AnimalSpecie>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);
        Task<AnimalSpecie?> GetByIdAsync(Guid id);
        Task<NewSpecieDropdownsVM> GetNewAnimalDropdownsVM();
    }
}