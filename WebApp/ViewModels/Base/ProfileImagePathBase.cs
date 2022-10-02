using WebClientApp.Interfaces;

namespace WebClientApp.ViewModels.Base
{
    public abstract class ProfileImagePathBase : IProfileImagePathBase
    {
        public string ProfileImgPath { get; set; } = string.Empty;
    }
}
