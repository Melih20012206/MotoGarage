using Microsoft.EntityFrameworkCore;
using MotoGarage.Models;

namespace MotoGarage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Motorcycle> Motorcycles { get; set; } = null!;

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<ServiceRecord> ServiceRecords { get; set; } = null!;
    }
}