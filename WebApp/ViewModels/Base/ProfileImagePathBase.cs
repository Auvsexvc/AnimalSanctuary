using WebApp.Interfaces;

namespace WebApp.ViewModels.Base
{
    public abstract class ProfileImagePathBase : IProfileImagePathBase
    {
        public string ProfileImgPath { get; set; } = string.Empty;
    }
}
