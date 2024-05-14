namespace cinemaServer.Models.Request.Post
{
    public class PostMovieDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Runtime { get; set; }

        public int Year { get; set; }

        public ERatings Rating { get; set; }
    }
}
