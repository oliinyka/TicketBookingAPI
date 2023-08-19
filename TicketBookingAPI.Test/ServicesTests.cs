using Microsoft.EntityFrameworkCore;
using TicketBookingAPI.Models;
using TicketBookingAPI.Persistance;
using TicketBookingAPI.Services;
using Xunit;

namespace TicketBookingAPI.Test
{
    public class ServicesTests
    {
        [Fact]
        public void MovieService_AddMovie_ShouldAddMovie()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TicketBookingContext>()
                .UseInMemoryDatabase(databaseName: "MovieServiceTestDB")
                .Options;

            using (var context = new TicketBookingContext(options))
            {
                var service = new MovieService(context);

                // Act
                service.AddMovie(new Movie { Title = "Test Movie", Description = "Test Description" , Genre = "Test Genre"});

                // Assert
                Assert.Equal(1, context.Movies.Count());
            }
        }

        [Fact]
        public void TheaterService_AddTheater_ShouldAddTheater()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TicketBookingContext>()
                .UseInMemoryDatabase(databaseName: "TheaterServiceTestDB")
                .Options;

            using (var context = new TicketBookingContext(options))
            {
                var service = new TheaterService(context);

                // Act
                service.AddTheaterAsync(new Theater { Name = "Test Theater" });

                // Assert
                Assert.Equal(1, context.Theaters.Count());
            }
        }

        [Fact]
        public async void ReservationService_CreateReservation_ShouldCreateReservation()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TicketBookingContext>()
                .UseInMemoryDatabase(databaseName: "ReservationServiceTestDB")
                .Options;

            using (var context = new TicketBookingContext(options))
            {
                var movie = new Movie { Title = "Test Movie", Description = "Test Description", Genre = "Test Genre" };
                context.Movies.Add(movie);

                var theater = new Theater { Name = "Test Theater", SeatingArrangement = new List<Row>() { new Row() { RowNumber = 1, NumberOfSeats = 3} } };
                context.Theaters.Add(theater);
                context.SaveChanges();

                var showTimeService = new ShowtimeService(context);
                var showtime = await showTimeService.AddShowtimeAsync(theater.Id, movie.Id, DateTime.Now);

                var service = new ReservationService(context);

                // Act
                var reservation = await service.CreateReservationAsync(showtime.Id, new List<Seat> { new Seat { RowNumber = 1, SeatNumber = 1 } });

                // Assert
                Assert.NotNull(reservation);
                Assert.Equal(1, context.Reservations.Count());
                Assert.Equal(1, reservation.ReservedSeats.Count());
            }
        }

        [Fact]
        public async void ShowtimeService_AddShowtime_ShouldAddShowtime()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TicketBookingContext>()
                .UseInMemoryDatabase(databaseName: "ShowtimeServiceTestDB")
                .Options;

            using (var context = new TicketBookingContext(options))
            {
                var movie = new Movie { Title = "Test Movie", Description = "Test Description", Genre = "Test Genre" };
                context.Movies.Add(movie);

                var theater = new Theater { Name = "Test Theater" };
                context.Theaters.Add(theater);
                context.SaveChanges();

                var service = new ShowtimeService(context);

                // Act
                var showtime = await service.AddShowtimeAsync(theater.Id, movie.Id, DateTime.Now);

                // Assert
                Assert.NotNull(showtime);
                Assert.Equal(1, context.Showtimes.Count());
            }
        }
    }
}