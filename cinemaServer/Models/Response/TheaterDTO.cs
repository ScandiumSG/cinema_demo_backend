using cinemaServer.Models.PureModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using cinemaServer.Models.Response.SeatResponse;

namespace cinemaServer.Models.Response
{
    public class TheaterDTO
    {
        public int Id { get; set; }

        public int Capacity { get; set; }

        public string Name { get; set; }

        public ICollection<SeatIncludedWithTheaterDTO> Seats { get; set; }
    }
}
