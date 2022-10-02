using WebClientApp.ViewModels;

namespace WebClientApp.Interfaces
{
    public interface IImageService
    {
        Task<ImageViewModel?> GetByIdAsync(Guid id);
        Task UploadImageAsync(IFormFile file, Guid id, string accessToken);
    }
}