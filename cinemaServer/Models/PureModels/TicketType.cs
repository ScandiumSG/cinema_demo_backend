using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace cinemaServer.Models.PureModels
{
    [Table("ticket_types")]
    public class TicketType
    {
        [Key]
        [Column("ticket_type_id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("name")]
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [Column("cost_hundreth")]
        [JsonPropertyName("price")]
        public int Price { get; set; }

        [Column("active_ticket_type")]
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        [Column("type_description")]
        [JsonPropertyName("description")]
        public required string Description { get; set; }
    }
}
