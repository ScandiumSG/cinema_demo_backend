using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels.People
{
    public class Customer : IPerson
    {
        [Key]
        [Column("customer_id")]
        public string Id { get; set; }

        [Column("name")]
        public string Name {  get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("country")]
        public string Country { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
