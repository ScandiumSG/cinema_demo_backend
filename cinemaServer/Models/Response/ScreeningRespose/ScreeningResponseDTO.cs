using cinemaServer.Models.PureModels;

namespace cinemaServer.Models.Response.ScreeningRespose
{
    public class ScreeningResponseDTO
    {
        public int Id { get; set; }

        public Movie? Movie { get; set; }

        public TheaterDTO? Theater { get; set; }

        public required ICollection<Ticket> Tickets { get; set; }

        public DateTime StartTime { get; set; }
    }
}
