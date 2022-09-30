using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Extensions
{
    public static class ImagesExtension
    {
        public static ImageViewModel ToViewModel(this Image image)
        {
            return new()
            {
                Id = image.Id,
                Path = image.Path,
                ContextId = image.ContextId
            };
        }
    }
}