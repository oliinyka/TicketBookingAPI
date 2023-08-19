using Microsoft.EntityFrameworkCore;
using TicketBookingAPI.Models;
using TicketBookingAPI.Persistance;

namespace TicketBookingAPI.Services
{
    public class TheaterService
    {
        private readonly TicketBookingContext _context;

        public TheaterService(TicketBookingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Theater>> GetTheatersAsync()
        {
            return await _context.Theaters.Include(t => t.Showtimes).ToListAsync();
        }

        public async Task<Theater> AddTheaterAsync(Theater theater)
        {
            await _context.Theaters.AddAsync(theater);
            await _context.SaveChangesAsync();
            return theater;
        }
    }
}
