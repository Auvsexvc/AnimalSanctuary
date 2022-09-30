using System.ComponentModel;

namespace WebApp.Models
{
    public abstract class UpdateProfileImage
    {
        [DisplayName("Profile image path")]
        public string ProfileImgPath { get; set; } = string.Empty;

        [DisplayName("Profile image")]
        public IFormFile? ProfileImg { get; set; }
    }
}
