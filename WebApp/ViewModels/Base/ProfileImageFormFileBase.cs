using WebApp.Interfaces;

namespace WebApp.ViewModels.Base
{
    public abstract class ProfileImageFormFileBase : IProfileImageFormFileBase
    {
        public IFormFile? ProfileImg { get; set; }
    }
}
