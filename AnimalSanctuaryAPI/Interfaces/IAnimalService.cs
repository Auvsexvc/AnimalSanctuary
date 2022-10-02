using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAnimalService
    {
        Task<AnimalViewModel?> AddAsync(AnimalDto dto);

        Task<Guid?> DeleteAsync(Guid id);

        Task<AnimalViewModel?> GetByIdAsync(Guid id);

        Task<IEnumerable<AnimalViewModel>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalViewModel?> UpdateAsync(Guid id, AnimalDto dto);
    }
}