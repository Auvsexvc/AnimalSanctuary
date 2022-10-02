using System.ComponentModel;

namespace WebClientApp.Data
{
    public sealed class AnimalType
    {
        public Guid Id { get; set; }

        [DisplayName("Type")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}