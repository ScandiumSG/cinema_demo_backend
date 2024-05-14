namespace cinemaServer.Models.Request.Put
{
    public class PutScreeningDTO
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public int TheaterId { get; set; }

        public DateTime StartTime { get; set; }
    }
}
