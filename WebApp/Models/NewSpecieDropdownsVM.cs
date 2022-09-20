using WebApp.Data;

namespace WebApp.Models
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