using System.ComponentModel;

namespace WebClientApp.ViewModels
{
    public sealed class ImageViewModel
    {
        [DisplayName("Image ID")]
        public Guid Id { get; set; }

        [DisplayName("Image path")]
        public string Path { get; set; } = string.Empty;

        [DisplayName("Context ID")]
        public Guid ContextId { get; set; }
    }
}