namespace TicketBookingAPI.Models
{
    public class Theater
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Row> SeatingArrangement { get; set; }
        public List<Showtime>? Showtimes { get; set; }
    }
}
