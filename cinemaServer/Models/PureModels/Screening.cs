using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels
{
    [Table("screenings_archive")]
    public class Screening
    {
        [Column("screening_id")]
        public int Id { get; set; }

        [ForeignKey("MovieId")]
        [Column("movie_id")]
        public int MovieId { get; set; }

        public Movie? Movie { get; set; }

        [ForeignKey("TheaterId")]
        [Column("theater_id")]
        public int TheaterId { get; set; }

        public Theater? Theater { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public DateTime StartTime { get; set; }
    }
}
