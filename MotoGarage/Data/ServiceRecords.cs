using System.ComponentModel.DataAnnotations;

namespace MotoGarage.Models
{
    public class ServiceRecord
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ServiceType { get; set; } = null!;

        [Required]
        public DateTime ServiceDate { get; set; }

        [Range(0, 100000)]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public int MotorcycleId { get; set; }

        public Motorcycle Motorcycle { get; set; } = null!;
    }
}