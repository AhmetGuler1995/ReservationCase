using Microsoft.EntityFrameworkCore;
using Reservation.Domain.Entities;

namespace Reservation.Persistence
{
    public class ReservationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ReservationDb");
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Domain.Entities.Reservation> Reservations { get; set; }
    }
}
