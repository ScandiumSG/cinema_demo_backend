namespace cinemaServer.Models.Request.Put
{
    public class PutTheaterDTO
    {
        public int Id { get; set; }

        public int? Capacity { get; set; }

        public required string Name { get; set; }
    }
}
