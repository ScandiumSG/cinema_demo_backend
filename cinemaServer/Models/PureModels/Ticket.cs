﻿using cinemaServer.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cinemaServer.Models.PureModels
{
    [Table("tickets")]
    public class Ticket
    {
        [Key]
        [Column("ticket_id")]
        public int Id { get; set; }

        [Column("screening_id")]
        public int ScreeningId { get; set; }

        [Column("movie_id")]
        public int MovieId { get; set; }

        [Column("theater_id")]
        public int TheaterId { get; set; }

        public Screening? Screening { get; set; }

        [Column("customer_id")]
        public string? CustomerId { get; set; }

        public ApplicationUser? Customer {  get; set; }

        [Column("seat_id")]
        public int SeatId { get; set; }

        public Seat? Seat { get; set; }

        [Column("ticket_type_id")]
        public int TicketTypeId { get; set; } = 1;

        public TicketType? TicketType { get; set; }
    }
}
