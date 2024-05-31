using cinemaServer.Models.Authentication;
using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.ScreeningRespose;
using cinemaServer.Models.Response.SeatResponse;
using cinemaServer.Models.Response.TheaterResponse;
using cinemaServer.Models.Response.TicketResponse;
using cinemaServer.Models.Response.UserResponse;
using cinemaServer.Models.User;

namespace cinemaServer.Services
{
    public static class ResponseConverter
    {
        public static ScreeningResponseDTO ConvertScreening(Screening screening) 
        {
            return new ScreeningResponseDTO()
            {
                Id = screening.Id,
                Movie = screening.Movie,
                Theater = ConvertTheaterToDTO(screening.Theater),
                TicketsSold = screening.Tickets.Count(),
                Tickets = screening.Tickets.Select(ConvertTicketToScreeningDTO).ToList(),
                StartTime = screening.StartTime,
            };
        }

        public static ScreeningResponseShortenedDTO ConvertShortenedScreening(Screening screening) 
        {
            return new ScreeningResponseShortenedDTO()
            {
                Id = screening.Id,
                Movie = screening.Movie,
                Theater = ConvertTheaterToDTO(screening.Theater!),
                TicketsSold = screening.Tickets.Count,
                StartTime = screening.StartTime,
            };
        }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        public static TheaterDTO ConvertTheaterToDTO(Theater? theater) 
        {
            return new TheaterDTO()
            {
                Id = theater.Id,
                Capacity = theater.Capacity,
                Name = theater.Name,
                Seats = theater.Seats.Select((s) => ConvertSeatToTheaterAccompanyDTO(s)).ToList(),
            };
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        public static SeatIncludedWithTheaterDTO ConvertSeatToTheaterAccompanyDTO(Seat seat) 
        {
            return new SeatIncludedWithTheaterDTO()
            {
                Id = seat.Id,
                Row = seat.Row,
                SeatNumber = seat.SeatNumber,
            };
        }

        public static TheaterShortenedDTO ConvertTheaterToShortenedDTO(Theater theater) 
        {
            return new TheaterShortenedDTO()
            {
                Id = theater.Id,
                Capacity = theater.Capacity,
                Name = theater.Name,
            };
        }

        public static UserChangeDTO ConvertApplicationUserToDTO(ApplicationUser user) 
        {
            return new UserChangeDTO()
            {
                Email = user.Email!,
                Username = user.UserName!,
            };
        }

        public static AuthResponse ConvertApplicationUserToAuthResponse(ApplicationUser user) 
        {
            return new AuthResponse()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
        }

        public static TicketInScreeningDTO ConvertTicketToScreeningDTO(Ticket ticket) 
        {
            return new TicketInScreeningDTO()
            {
                Id = ticket.Id,
                Seat = ConvertSeatToTheaterAccompanyDTO(ticket.Seat!),
            };
        }
    }
}
