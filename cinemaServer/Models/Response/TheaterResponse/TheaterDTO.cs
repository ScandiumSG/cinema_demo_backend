using cinemaServer.Models.Response.SeatResponse;

namespace cinemaServer.Models.Response.TheaterResponse
{
    public class TheaterDTO
    {
        public int Id { get; set; }

        public int Capacity { get; set; }

        public string? Name { get; set; }

        public ICollection<SeatIncludedWithTheaterDTO> Seats { get; set; } = new List<SeatIncludedWithTheaterDTO>();
    }
}
