using System.ComponentModel.DataAnnotations;
using System.Data;

namespace cinemaServer.Models.Authentication
{
    public class RegistrationRequest
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        public ERole Role { get; set; } = ERole.User;

        public bool IsValid()
        {
            return true;
        }
    }
}
