namespace WebApp.Dtos
{
    public class ImageDto
    {
        public IFormFile? Image { get; set; }
        public Guid ContextId { get; set; }
    }
}