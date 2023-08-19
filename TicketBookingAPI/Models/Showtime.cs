namespace TicketBookingAPI.Models
{
    public class Showtime
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime ShowDateTime { get; set; }
        public List<Seat> Seats { get; set; } = new List<Seat>();
    }
}
