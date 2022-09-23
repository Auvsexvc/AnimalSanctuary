using WebApp.Data;

namespace WebApp.ViewModels
{
    public class NewAnimalDropdownsVM
    {
        public List<AnimalSpecie>? Species { get; set; }
        public List<Facility>? Facilities { get; set; }
        public List<Data.AnimalType>? Types { get; set; }

        public NewAnimalDropdownsVM()
        {
            Species = new();
            Facilities = new();
            Types = new();
        }
    }
}