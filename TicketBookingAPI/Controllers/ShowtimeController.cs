using Microsoft.AspNetCore.Mvc;
using TicketBookingAPI.Models;
using TicketBookingAPI.Services;

namespace TicketBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShowtimeController : ControllerBase
    {
        private readonly ShowtimeService _showtimeService;

        public ShowtimeController(ShowtimeService showtimeService)
        {
            _showtimeService = showtimeService;
        }

        /// <summary>
        /// Adds a showtime
        /// </summary>
        /// <param name="request">Showtime details</param>
        /// <returns>200 in case of success</returns>
        /// <returns>400 in case of errors in request</returns>
        [HttpPost("add")]
        public async Task<ActionResult<Showtime>> AddShowtime(ShowtimeRequest request)
        {
            try
            {
                var showtime = await _showtimeService.AddShowtimeAsync(
                    request.TheaterId,
                    request.MovieId,
                    request.ShowDateTime);

                return Ok(showtime);
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ShowtimeRequest
    {
        public int TheaterId { get; set; }
        public int MovieId { get; set; }
        public DateTime ShowDateTime { get; set; }
    }
}

