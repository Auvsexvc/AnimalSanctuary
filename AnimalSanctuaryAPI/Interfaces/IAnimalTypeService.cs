using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAnimalTypeService
    {
        Task<AnimalTypeViewModel?> Add(AnimalTypeDto dto);

        Task<Guid?> Delete(Guid id);

        Task<AnimalTypeViewModel?> GetViewModel(Guid id);

        Task<IEnumerable<AnimalTypeViewModel>?> GetViewModels(string? sortingField, string? sortingOrder, string? filteringString);

        Task<AnimalTypeViewModel?> Update(Guid id, AnimalTypeDto dto);
    }
}