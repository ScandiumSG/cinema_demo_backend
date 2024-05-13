namespace cinemaServer.Models.Request
{
    public class PostScreeningDTO
    {
        public int MovieId { get; set; }

        public int TheaterId { get; set; }

        public DateTime StartTime { get; set; }
    }
}
