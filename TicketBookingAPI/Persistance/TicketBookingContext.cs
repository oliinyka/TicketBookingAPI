using Microsoft.EntityFrameworkCore;
using TicketBookingAPI.Models;

namespace TicketBookingAPI.Persistance
{
    public class TicketBookingContext : DbContext
    {
        public TicketBookingContext(DbContextOptions<TicketBookingContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<Theater> Theaters { get; set; } = null!;
        public DbSet<Showtime> Showtimes { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;

    }
}
