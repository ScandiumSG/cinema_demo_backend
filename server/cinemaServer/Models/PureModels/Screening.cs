using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels
{
    [Table("movie_screenings")]
    public class Screening
    {
        [Column("screening_id")]
        public int Id { get; set; }

        [ForeignKey("movie_id")]
        [Column("movie_id")]
        public int MovieId { get; set; }

        [ForeignKey("theater_id")]
        [Column("theater_id")]
        public int TheaterId { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public DateTime StartTime { get; set; }
    }
}
