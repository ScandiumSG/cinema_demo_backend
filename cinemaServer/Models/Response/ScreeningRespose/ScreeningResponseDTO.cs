﻿using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.TheaterResponse;
using cinemaServer.Models.Response.TicketResponse;

namespace cinemaServer.Models.Response.ScreeningRespose
{
    public class ScreeningResponseDTO
    {
        public int Id { get; set; }

        public Movie? Movie { get; set; }

        public TheaterShortenedDTO? Theater { get; set; }

        public int TicketsSold { get; set; }

        public required ICollection<TicketInScreeningDTO> Tickets { get; set; }

        public DateTime StartTime { get; set; }
    }
}
