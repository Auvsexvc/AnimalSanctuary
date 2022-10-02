using System.ComponentModel;

namespace WebClientApp.Data
{
    public sealed class AnimalSpecie
    {
        public Guid Id { get; set; }

        [DisplayName("Specie")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public Guid TypeId { get; set; }
    }
}