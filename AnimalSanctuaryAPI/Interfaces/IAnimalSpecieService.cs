using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAnimalSpecieService
    {
        Task<AnimalSpecieViewModel?> AddAsync(AnimalSpecieDto dto);

        Task<Guid?> DeleteAsync(Guid id);

        Task<AnimalSpecieViewModel?> GetByIdAsync(Guid id);

        Task<IEnumerable<AnimalSpecieViewModel>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalSpecieViewModel?> UpdateAsync(Guid id, AnimalSpecieDto dto);
    }
}