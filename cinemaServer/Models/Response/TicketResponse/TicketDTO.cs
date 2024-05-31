using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.ScreeningRespose;
using cinemaServer.Models.Response.SeatResponse;
using cinemaServer.Models.Response.UserResponse;
using cinemaServer.Models.User;

namespace cinemaServer.Models.Response.TicketResponse
{
    public class TicketDTO
    {
        public int Id { get; set; }

        public int ScreeningId { get; set; }

        public ScreeningResponseForTicketDTO Screening { get; set; }

        public UserOnTicketDTO Customer { get; set; }

        public SeatIncludedWithTheaterDTO? Seat { get; set; }
    }
}
