using WebClientApp.Interfaces;

namespace WebClientApp.ViewModels.Base
{
    public abstract class ProfileImageFormFileBase : IProfileImageFormFileBase
    {
        public IFormFile? ProfileImg { get; set; }
    }
}
