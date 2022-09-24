using System.ComponentModel;
using WebApp.Data;
using WebApp.Models.Base;

namespace WebApp.Models
{
    public class AnimalSpecieViewModel : IModelBase
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [DisplayName("Type")]
        public string TypeName { get; set; } = String.Empty;
    }
}