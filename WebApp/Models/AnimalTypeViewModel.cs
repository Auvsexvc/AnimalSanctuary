using System.ComponentModel;
using WebApp.Models.Base;

namespace WebApp.Models
{
    public class AnimalTypeViewModel : IModelBase
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        [DisplayName("Type")]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }
    }
}