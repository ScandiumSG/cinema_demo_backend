namespace cinemaServer.Models.Request.Put
{
    public class PutUserDTO
    {
        public required string Id { get; set; }

        public required string Email { get; set; }

        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
