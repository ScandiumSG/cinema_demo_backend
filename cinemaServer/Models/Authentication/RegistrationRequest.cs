using System.ComponentModel.DataAnnotations;
using System.Data;

namespace cinemaServer.Models.Authentication
{
    public class RegistrationRequest
    {
        private string? _email;
        private string? _username;

        [Required]
        public string? Email { get { return _email; } set { _email = value.ToLower(); } }

        [Required]
        public string? Username { get { return _username; } set { _username = value.ToLower(); } }

        [Required]
        public string? Password { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}
