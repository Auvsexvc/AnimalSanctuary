using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAnimalTypeService
    {
        Task<AnimalTypeViewModel?> AddAsync(AnimalTypeDto dto);

        Task<Guid?> DeleteAsync(Guid id);

        Task<AnimalTypeViewModel?> GetByIdAsync(Guid id);

        Task<IEnumerable<AnimalTypeViewModel>?> GetAllAsync(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalTypeViewModel?> UpdateAsync(Guid id, AnimalTypeDto dto);
    }
}