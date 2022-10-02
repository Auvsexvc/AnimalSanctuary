using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Entities
{
    public sealed class AnimalType
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public List<AnimalSpecie>? Species { get; set; }
    }
}