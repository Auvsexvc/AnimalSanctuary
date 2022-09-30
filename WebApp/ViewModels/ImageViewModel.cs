using System.ComponentModel;

namespace WebApp.ViewModels
{
    public class ImageViewModel
    {
        [DisplayName("Image ID")]
        public Guid Id { get; set; }

        [DisplayName("Image path")]
        public string Path { get; set; } = string.Empty;

        [DisplayName("Context ID")]
        public Guid ContextId { get; set; }
    }
}