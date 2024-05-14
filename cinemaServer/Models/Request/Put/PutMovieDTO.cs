namespace cinemaServer.Models.Request.Put
{
    public class PutMovieDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? Runtime { get; set; }

        public int? Year { get; set; }

        public ERatings? Rating { get; set; }
    }
}
