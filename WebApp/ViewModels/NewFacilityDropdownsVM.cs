using WebApp.Data;

namespace WebApp.ViewModels
{
    public sealed class NewFacilityDropdownsVM
    {
        public List<Animal>? Animals { get; set; }

        public NewFacilityDropdownsVM()
        {
            Animals = new();
        }
    }
}