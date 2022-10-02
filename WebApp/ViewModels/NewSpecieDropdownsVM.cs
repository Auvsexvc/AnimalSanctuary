namespace WebApp.ViewModels
{
    public sealed class NewSpecieDropdownsVM
    {
        public List<Data.AnimalType>? Types { get; set; }

        public NewSpecieDropdownsVM()
        {
            Types = new();
        }
    }
}