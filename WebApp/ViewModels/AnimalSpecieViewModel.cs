using System.ComponentModel;
using WebApp.ViewModels.Base;

namespace WebApp.ViewModels
{
    public class AnimalSpecieViewModel : IBaseViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Type")]
        public string TypeName { get; set; } = string.Empty;
    }
}