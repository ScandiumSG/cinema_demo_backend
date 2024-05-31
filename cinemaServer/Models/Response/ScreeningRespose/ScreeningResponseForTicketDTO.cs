using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.TheaterResponse;

namespace cinemaServer.Models.Response.ScreeningRespose
{
    public class ScreeningResponseForTicketDTO
    {
        public int Id { get; set; }

        public Movie? Movie { get; set; }

        public TheaterShortenedDTO? Theater { get; set; }

        public DateTime StartTime { get; set; }
    }
}
