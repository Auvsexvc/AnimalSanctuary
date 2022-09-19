namespace WebApp.Models
{
    public class AnimalSpecieViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        public string TypeName { get; set; } = String.Empty;

        public Guid TypeId { get; set; }
    }
}