namespace AnimalSanctuaryAPI.ViewModels
{
    public sealed class AnimalSpecieViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public Guid TypeId { get; set; }
    }
}