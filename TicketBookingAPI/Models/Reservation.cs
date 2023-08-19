namespace TicketBookingAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Showtime Showtime { get; set; }
        public List<Seat> ReservedSeats { get; set; } = new List<Seat>();
        public bool Confirmed { get; set; }
        public DateTime ReservationTime { get; set; }

    }
}
