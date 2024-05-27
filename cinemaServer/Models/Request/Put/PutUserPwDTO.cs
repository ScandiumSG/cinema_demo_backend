namespace cinemaServer.Models.Request.Put
{
    public class PutUserPwDTO
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
