namespace WebApp.Models
{
    public class AnimalSortingDropdowns
    {
        public List<string> Fields { get; set; }
        public List<string> Order { get; set; }

        public AnimalSortingDropdowns()
        {
            Fields = new();
            Order = new();
        }
    }
}
