using Microsoft.AspNetCore.Mvc;
using TicketBookingAPI.Models;
using TicketBookingAPI.Services;

namespace TicketBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Creates a reservation for a specified showtime
        /// </summary>
        /// <param name="request">Request with showtime and seats details</param>
        /// <returns>200 in case of success</returns>
        /// <returns>400 in case of errors in request</returns>
        [HttpPost("create")]
        public async Task<ActionResult<Reservation>> CreateReservation(ReservationRequest request)
        {
            var seats = new List<Seat>();
            foreach (var seat in request.Seats)
            {
                seats.Add(new Seat { RowNumber = seat.RowNumber, SeatNumber = seat.SeatNumber });
            }

            try
            {
                var reservation = await _reservationService.CreateReservationAsync(request.ShowtimeId, seats);
                return Ok(reservation);
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ReservationRequest
    {
        public int ShowtimeId { get; set; }
        public List<SeatRequest> Seats { get; set; }
    }

    public class SeatRequest
    {
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
    }

}
