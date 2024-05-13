using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels
{
    public class Theater
    {
        [Key]
        [Column("theater_id")]
        public int Id { get; set; }

        public int Capacity { get; set; }

        public string Name { get; set; }
    }
}
