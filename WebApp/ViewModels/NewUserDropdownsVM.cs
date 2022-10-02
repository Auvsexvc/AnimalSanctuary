using WebClientApp.Data;

namespace WebClientApp.ViewModels
{
    public sealed class NewUserDropdownsVM
    {
        public List<Role> Roles { get; set; }

        public NewUserDropdownsVM()
        {
            Roles = new();
        }
    }
}