using System.ComponentModel;

namespace WebApp.Models
{
    public class AnimalTypeViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        [DisplayName("Type")]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }
    }
}