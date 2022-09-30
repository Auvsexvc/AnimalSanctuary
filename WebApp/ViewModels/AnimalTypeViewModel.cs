using System.ComponentModel;
using WebApp.ViewModels.Base;

namespace WebApp.ViewModels
{
    public class AnimalTypeViewModel : IBaseViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        [DisplayName("Type")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Description")]
        public string? Description { get; set; }
    }
}