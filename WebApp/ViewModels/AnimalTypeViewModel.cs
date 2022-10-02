using System.ComponentModel;
using WebClientApp.Interfaces;

namespace WebClientApp.ViewModels
{
    public sealed class AnimalTypeViewModel : IBaseViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        [DisplayName("Type")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Description")]
        public string? Description { get; set; }
    }
}