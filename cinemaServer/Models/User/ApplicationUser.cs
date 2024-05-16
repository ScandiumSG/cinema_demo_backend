using cinemaServer.Models.PureModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;



namespace cinemaServer.Models.User
{
    [Table("users")]
    public class ApplicationUser : IdentityUser
    {
        [Column("role")]
        public ERole Role { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
