namespace WebApp.ViewModels
{
    public sealed class SortingDropdownsViewModel
    {
        public List<string> Fields { get; set; }
        public Dictionary<string, string> DisplayNames { get; set; }
        public List<string> Order { get; set; }

        public SortingDropdownsViewModel()
        {
            Fields = new();
            DisplayNames = new();
            Order = new();
        }
    }
}