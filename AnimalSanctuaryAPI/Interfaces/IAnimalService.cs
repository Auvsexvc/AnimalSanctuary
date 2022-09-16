using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAnimalService
    {
        Task<AnimalViewModel?> Add(AnimalDto dto);
        Task<Guid?> Delete(Guid id);
        Task<AnimalViewModel?> GetViewModel(Guid id);
        Task<IEnumerable<AnimalViewModel>?> GetViewModels(string? sortingField, string? sortingOrder, string? filteringString);
        Task<AnimalViewModel?> Update(Guid id, AnimalDto dto);
    }
}