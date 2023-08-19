using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBookingAPI.Models;
using TicketBookingAPI.Persistance;

namespace TicketBookingAPI.Services
{
    public class ReservationService
    {
        private readonly TicketBookingContext _context;

        public ReservationService(TicketBookingContext context)
        {
            _context = context;
        }

        public async Task<Reservation> CreateReservationAsync(int showtimeId, List<Seat> seats)
        {
            var showtime = await _context.Showtimes
                .Include(s => s.Seats)
                .FirstOrDefaultAsync(s => s.Id == showtimeId);

            if (showtime == null)
            {
                throw new InvalidOperationException($"The showtime {showtimeId} is not available");
            }

            foreach (var seat in seats)
            {
                if (!IsSeatAvailable(showtime, seat))
                {
                    throw new InvalidOperationException($"The seat {seat.RowNumber} , {seat.SeatNumber} is not available");
                }

                var reservedSeat = showtime.Seats.Where(s => s.SeatNumber == seat.SeatNumber
                                                      && s.RowNumber == seat.RowNumber).FirstOrDefault();

                reservedSeat.SeatStatus = SeatStatus.Reserved;
            }

            var reservation = new Reservation
            {
                Showtime = showtime,
                ReservedSeats = seats,
                ReservationTime = DateTime.UtcNow
            };

            _context.Reservations.Add(reservation);
            _context.SaveChanges();

            return reservation;
        }

        private bool IsSeatAvailable(Showtime showtime, Seat seat)
        {
            var showtimeSeat = showtime.Seats.Where(s => s.SeatNumber == seat.SeatNumber
                                                      && s.RowNumber == seat.RowNumber)
                                              .FirstOrDefault();

            if (showtimeSeat == null || showtimeSeat.SeatStatus != SeatStatus.Available)
                return false;

            return true;
        }
    }
}
