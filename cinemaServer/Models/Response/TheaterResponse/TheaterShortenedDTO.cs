using cinemaServer.Models.Response.SeatResponse;

namespace cinemaServer.Models.Response.TheaterResponse
{
    public class TheaterShortenedDTO
    {
        public int Id { get; set; }

        public int Capacity { get; set; }

        public string? Name { get; set; }
    }
}
