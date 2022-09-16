namespace WebApp.Data
{
    public class AnimalSpecie
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? TypeName { get; set; }

        public Guid TypeId { get; set; }
    }
}
