using WebApp.Data;

namespace WebApp.ViewModels
{
    public class NewSpecieDropdownsVM
    {
        public List<AnimalType>? Types { get; set; }

        public NewSpecieDropdownsVM()
        {
            Types = new();
        }
    }
}