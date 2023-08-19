using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicketBookingAPI.Models;
using TicketBookingAPI.Persistance;

namespace TicketBookingAPI.Services
{
    public class ShowtimeService
    {
        private readonly TicketBookingContext _context;

        public ShowtimeService(TicketBookingContext context)
        {
            _context = context;
        }

        public async Task<Showtime> AddShowtimeAsync(int theaterId, int movieId, DateTime dateTime)
        {
            var theater = await _context.Theaters
                .Include(t => t.Showtimes)
                .Include(t => t.SeatingArrangement)
                .FirstOrDefaultAsync(t => t.Id == theaterId);
            var movie = await _context.Movies.FindAsync(movieId);

            if (theater == null)
            {
                throw new InvalidOperationException($"The theater {theaterId} is not available");
            }

            if (movie == null)
            {
                throw new InvalidOperationException($"The movie {movieId} is not available");
            }

            var seats = GetSeatsForTheatre(theater);

            var showtime = new Showtime
            {
                MovieId = movieId,
                ShowDateTime = dateTime,
                Seats = seats
            };

            theater.Showtimes.Add(showtime);
            await _context.SaveChangesAsync();

            return showtime;
        }

        private List<Seat> GetSeatsForTheatre(Theater theater)
        {
            List<Seat> seats = new List<Seat>();

            foreach (var row in theater.SeatingArrangement)
            {
                for(int i = 1;  i <= row.NumberOfSeats; i++) 
                {
                    seats.Add(new Seat() { RowNumber = row.RowNumber, SeatNumber = i });                                    
                }
            }

            return seats;
        }
    }
}
