using System.ComponentModel;

namespace WebApp.Data
{
    public sealed class AnimalSpecie
    {
        public Guid Id { get; set; }

        [DisplayName("Specie")]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        public string TypeName { get; set; } = String.Empty;

        public Guid TypeId { get; set; }
    }
}