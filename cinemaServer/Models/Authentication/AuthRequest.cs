namespace cinemaServer.Models.Authentication
{
    public class AuthRequest
    {
        private string _email;

        public string? Email { get { return _email; } set { _email = value.ToLower(); } }

        public string? Password { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
