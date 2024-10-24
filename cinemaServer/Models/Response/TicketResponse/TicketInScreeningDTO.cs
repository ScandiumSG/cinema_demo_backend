using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.SeatResponse;

namespace cinemaServer.Models.Response.TicketResponse
{
    public class TicketInScreeningDTO
    {
        public int Id { get; set; }

        public required SeatIncludedWithTheaterDTO Seat {  get; set; }

        public required TicketType TicketType { get; set; }
    }
}
