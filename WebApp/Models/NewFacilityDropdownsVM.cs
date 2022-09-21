using WebApp.Data;

namespace WebApp.Models
{
    public class NewFacilityDropdownsVM
    {
        public List<Animal>? Animals { get; set; }

        public NewFacilityDropdownsVM()
        {
            Animals = new();
        }
    }
}