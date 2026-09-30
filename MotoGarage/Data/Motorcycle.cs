using System.ComponentModel.DataAnnotations;

namespace MotoGarage.Models
{
    public class Motorcycle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = null!;

        [Range(1900, 2100)]
        public int Year { get; set; }

        [Range(1, 3000)]
        public int EngineCapacity { get; set; }

        [Range(0, 500000)]
        public int Kilometers { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}