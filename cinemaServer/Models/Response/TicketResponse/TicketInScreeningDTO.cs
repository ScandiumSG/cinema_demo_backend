using cinemaServer.Models.Response.SeatResponse;

namespace cinemaServer.Models.Response.TicketResponse
{
    public class TicketInScreeningDTO
    {
        public int Id { get; set; }
        
        public int ScreeningId { get; set; }

        public int MovieId { get; set; }

        public int TheaterId { get; set; }

        public int SeatId { get; set; }

        public required SeatIncludedWithTheaterDTO Seat {  get; set; }

    }
}
