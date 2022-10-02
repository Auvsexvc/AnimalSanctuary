using WebClientApp.Data;

namespace WebClientApp.ViewModels
{
    public sealed class NewSpecieDropdownsVM
    {
        public List<AnimalType>? Types { get; set; }

        public NewSpecieDropdownsVM()
        {
            Types = new();
        }
    }
}