using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAnimalSpecieService
    {
        Task<AnimalSpecieViewModel?> Add(AnimalSpecieDto dto);
        Task<Guid?> Delete(Guid id);
        Task<AnimalSpecieViewModel?> GetViewModel(Guid id);
        Task<IEnumerable<AnimalSpecieViewModel>?> GetViewModels(string? sortingField, string? sortingOrder, string? filteringString);
        Task<AnimalSpecieViewModel?> Update(Guid id, AnimalSpecieDto dto);
    }
}