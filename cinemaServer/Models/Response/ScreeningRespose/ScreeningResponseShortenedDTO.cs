using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.TheaterResponse;

namespace cinemaServer.Models.Response.ScreeningRespose
{
    public class ScreeningResponseShortenedDTO
    {
        public int Id { get; set; }

        public Movie? Movie { get; set; }

        public TheaterShortenedDTO? Theater { get; set; }

        public int TicketsSold { get; set; }

        public DateTime StartTime { get; set; }
    }
}
