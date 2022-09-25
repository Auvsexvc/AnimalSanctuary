namespace AnimalSanctuaryAPI.ViewModels
{
    public class AnimalSpecieViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        //public string? TypeName { get; set; }

        public Guid TypeId { get; set; }
    }
}