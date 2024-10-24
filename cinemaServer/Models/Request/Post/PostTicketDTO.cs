﻿namespace cinemaServer.Models.Request.Post
{
    public class PostTicketDTO
    {
        public int ScreeningId { get; set; }

        public int MovieId { get; set; }

        public required string CustomerId { get; set; }

        public ICollection<int> SeatId { get; set; } = new List<int>();

        public ICollection<int> TicketTypeId { get; set; } = new List<int>();
    }
}
