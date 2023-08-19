namespace TicketBookingAPI.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public SeatStatus SeatStatus { get; set; }
    }
}
