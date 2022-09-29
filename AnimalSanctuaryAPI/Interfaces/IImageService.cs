using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IImageService
    {
        Task<ImageViewModel?> Upload(IFormFile file, Guid id);

        Task<ImageViewModel?> GetByIdAsync(Guid id);

        Task<IEnumerable<ImageViewModel>> GetAllAsync();
        Task<ImageViewModel?> Replace(IFormFile file, Guid id);
    }
}