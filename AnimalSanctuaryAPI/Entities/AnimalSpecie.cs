using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AnimalSanctuaryAPI.Entities
{
    public class AnimalSpecie
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [Required]
        [JsonIgnore]
        [ForeignKey("TypeId")]
        public AnimalType Type { get; set; } = new AnimalType();
    }
}