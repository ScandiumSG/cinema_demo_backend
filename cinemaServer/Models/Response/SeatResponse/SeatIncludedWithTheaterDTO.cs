using cinemaServer.Models.PureModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cinemaServer.Models.Response.SeatResponse
{
    public class SeatIncludedWithTheaterDTO
    {
        public int Id { get; set; }

        public int Row { get; set; }

        public int SeatNumber { get; set; }
    }
}
