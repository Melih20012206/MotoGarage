using System.ComponentModel.DataAnnotations;

namespace MotoGarage.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }

        public ICollection<Motorcycle> Motorcycles { get; set; }
            = new List<Motorcycle>();
    }
}
