using System.ComponentModel;
using WebClientApp.Interfaces;

namespace WebClientApp.ViewModels.Base
{
    public abstract class ProfileImageBase : IProfileImageFormFileBase, IProfileImagePathBase
    {
        [DisplayName("Profile image path")]
        public string ProfileImgPath { get; set; } = string.Empty;

        [DisplayName("Profile image")]
        public IFormFile? ProfileImg { get; set; }
    }
}
