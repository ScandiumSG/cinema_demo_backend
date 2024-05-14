using System.Data;

namespace cinemaServer.Models.Authentication
{
    public class AuthResponse
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public ERole? Role { get; set; }
        public string? Token { get; set; }
    }
}
