namespace WebClientApp.Dtos
{
    public sealed class ImageDto
    {
        public IFormFile? Image { get; set; }
        public Guid ContextId { get; set; }
    }
}