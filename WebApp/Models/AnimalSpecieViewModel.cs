using System.ComponentModel;
using WebApp.Data;

namespace WebApp.Models
{
    public class AnimalSpecieViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [DisplayName("Type")]
        public string TypeName { get; set; } = String.Empty;
    }
}