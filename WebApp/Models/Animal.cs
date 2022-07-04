using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime DateCreated { get; } = DateTime.Now;
        public string? Sex { get; set; }
        public string? HealthState { get; set; }
    }
}
