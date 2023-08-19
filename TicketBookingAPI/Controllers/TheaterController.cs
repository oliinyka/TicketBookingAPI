using Microsoft.AspNetCore.Mvc;
using TicketBookingAPI.Models;
using TicketBookingAPI.Services;

namespace TicketBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheaterController : ControllerBase
    {
        private readonly TheaterService _theaterService;

        public TheaterController(TheaterService theaterService)
        {
            _theaterService = theaterService;
        }

        /// <summary>
        /// Get all available theaters
        /// </summary>
        /// <returns>All available theaters</returns>
        [HttpGet("theaters")]
        public async Task<ActionResult<IEnumerable<Theater>>> GetTheaters()
        {
            var theaters = _theaterService.GetTheatersAsync();
            return Ok(theaters);
        }

        /// <summary>
        /// Creates a theater
        /// </summary>
        /// <param name="theater">Theater to create</param>
        /// <returns>Created theater</returns>
        [HttpPost("theaters")]
        public async Task<ActionResult<Theater>> AddTheater(Theater theater)
        {
            var addedTheater = _theaterService.AddTheaterAsync(theater);
            return CreatedAtAction(nameof(GetTheaters), new { id = addedTheater.Id }, addedTheater);
        }
    }
}
