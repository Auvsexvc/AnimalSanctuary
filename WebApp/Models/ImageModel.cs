namespace WebApp.Models
{
    public class ImageModel
    {
        public Guid Id { get; set; }

        public string Path { get; set; } = string.Empty;

        public Guid ContextId { get; set; }
    }
}