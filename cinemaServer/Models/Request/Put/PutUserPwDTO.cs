namespace cinemaServer.Models.Request.Put
{
    public class PutUserPwDTO
    {
        public required string Id { get; set; }

        public required string OldPassword { get; set; }

        public required string NewPassword { get; set; }
    }
}
