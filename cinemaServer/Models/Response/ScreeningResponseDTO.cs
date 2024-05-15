using cinemaServer.Models.PureModels;

namespace cinemaServer.Models.Response
{
    public class ScreeningResponseDTO
    {
        public int Id { get; set; }

        public Movie Movie { get; set; }

        public Theater Theater { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public DateTime StartTime { get; set; }
    }
}
