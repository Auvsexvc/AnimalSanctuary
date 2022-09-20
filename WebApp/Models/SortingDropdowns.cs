namespace WebApp.Models
{
    public class SortingDropdowns
    {
        public List<string> Fields { get; set; }
        public Dictionary<string,string> DisplayNames { get; set; }
        public List<string> Order { get; set; }

        public SortingDropdowns()
        {
            Fields = new();
            DisplayNames = new();
            Order = new();
        }
    }
}
