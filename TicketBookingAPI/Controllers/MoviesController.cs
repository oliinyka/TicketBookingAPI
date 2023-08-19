using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketBookingAPI.Models;
using TicketBookingAPI.Persistance;
using TicketBookingAPI.Services;

namespace TicketBookingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MovieController(MovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Get all available movies
        /// </summary>
        /// <returns>Returns all available movies</returns>
        [HttpGet("movies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _movieService.GetMovies();

            return Ok(movies);
        }

        /// <summary>
        /// Adds the movie
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Created movie</returns>
        [HttpPost("movies")]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            var addedMovie = await _movieService.AddMovie(movie);
            return CreatedAtAction(nameof(GetMovies), new { id = addedMovie.Id }, addedMovie);
        }

        /// <summary>
        /// Updates the movie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns>NoContent in case of success</returns>
        [HttpPut("movies/{id}")]
        public async Task<ActionResult> UpdateMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            await _movieService.UpdateMovie(movie);
            return NoContent();
        }

        /// <summary>
        /// Deletes the movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NoContent in case of success<</returns>
        [HttpDelete("movies/{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            await _movieService.DeleteMovie(id);
            return NoContent();
        }
    }
}
