using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels
{
    [Table("playable_movies")]
    public class Movie
    {
        [Key]
        [Column("movie_id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("runtime")]
        public int Runtime { get; set; }

        [Column("release_year")]
        public int Year { get; set; }

        //public ICollection<IPerson> Actors { get; set; } = new List<IPerson>();

        //public IPerson? Director { get; set; }

        [Column("rating")]
        public ERatings Rating { get; set; } = 0;
    }
}
