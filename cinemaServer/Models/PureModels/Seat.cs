using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels
{
    [Table("individual_seats")]
    public class Seat
    {
        [Key]
        [Column("seat_id")]
        public int Id { get; set; }

        [Column("theater_id")]
        public int TheaterId { get; set; }

        public Theater? Theater { get; set; }

        [Column("row")]
        public int Row {  get; set; }

        [Column("seat_number")]
        public int SeatNumber { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
