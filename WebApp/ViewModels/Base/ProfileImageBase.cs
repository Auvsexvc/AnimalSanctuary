using System.ComponentModel;
using WebApp.Interfaces;

namespace WebApp.ViewModels.Base
{
    public abstract class ProfileImageBase : IProfileImageFormFileBase, IProfileImagePathBase
    {
        [DisplayName("Profile image path")]
        public string ProfileImgPath { get; set; } = string.Empty;

        [DisplayName("Profile image")]
        public IFormFile? ProfileImg { get; set; }
    }
}
