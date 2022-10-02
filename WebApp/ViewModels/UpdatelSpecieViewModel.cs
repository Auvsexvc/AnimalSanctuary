using System.ComponentModel;
using WebClientApp.Interfaces;

namespace WebClientApp.ViewModels
{
    public sealed class UpdateSpecieViewModel : IBaseViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [DisplayName("Type ID")]
        public Guid TypeId { get; set; }

        [DisplayName("Type")]
        public Data.AnimalType Type { get; set; } = new Data.AnimalType();
    }
}