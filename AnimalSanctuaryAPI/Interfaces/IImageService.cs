using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IImageService
    {
        Task<ImageViewModel?> UploadAsync(IFormFile file, Guid id);

        Task<ImageViewModel?> GetByIdAsync(Guid id);

        Task<IEnumerable<ImageViewModel>> GetAllAsync();
    }
}