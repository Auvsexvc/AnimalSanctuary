using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Entities
{
    public sealed class Image
    {
        [Key]
        public Guid Id { get; set; }

        public string Path { get; set; } = string.Empty;

        public Guid ContextId { get; set; }
    }
}