using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IImageService
    {
        Task<ImageViewModel?> GetByIdAsync(Guid id);
        Task UploadImageAsync(IFormFile file, Guid id, string accessToken);
    }
}