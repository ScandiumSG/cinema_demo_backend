using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels
{
    [Table("theater_rooms")]
    public class Theater
    {
        [Key]
        [Column("theater_id")]
        public int Id { get; set; }

        [Column("capacity")]
        public int Capacity { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public ICollection<Seat> Seats { get; set; }
    }
}
