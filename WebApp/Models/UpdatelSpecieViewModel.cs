using System.ComponentModel;
using WebApp.Models.Base;

namespace WebApp.Models
{
    public class UpdateSpecieViewModel : IModelBase
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [DisplayName("Type ID")]
        public Guid TypeId { get; set; }

        [DisplayName("Type")]
        public Data.AnimalType Type { get; set; } = new Data.AnimalType();
    }
}