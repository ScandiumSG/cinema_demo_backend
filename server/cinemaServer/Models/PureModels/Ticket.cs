using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels
{
    public class Ticket
    {
        [Column("ticket_id")]
        public int Id { get; set; }

        [ForeignKey("ScreeningId")]
        [Column("screening_id")]
        public int ScreeningId { get; set; }

        [ForeignKey("CustomerId")]
        [Column("customer_id")]
        public string? CustomerId { get; set; }

        [Column("column")]
        public int Column { get; set; }

        [Column("row")]
        public int Row {  get; set; }
    }
}
