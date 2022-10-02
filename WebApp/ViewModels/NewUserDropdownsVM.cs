using WebApp.Data;

namespace WebApp.ViewModels
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