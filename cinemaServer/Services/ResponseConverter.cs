using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response;
using cinemaServer.Models.Response.ScreeningRespose;
using cinemaServer.Models.Response.SeatResponse;

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
                Tickets = screening.Tickets,
                StartTime = screening.StartTime,
            };
        }

        public static TheaterDTO ConvertTheaterToDTO(Theater theater) 
        {
            return new TheaterDTO()
            {
                Id = theater.Id,
                Capacity = theater.Capacity,
                Name = theater.Name,
                Seats = theater.Seats.Select((s) => ConvertSeatToTheaterAccompanyDTO(s)).ToList(),
            };
        }

        public static SeatIncludedWithTheaterDTO ConvertSeatToTheaterAccompanyDTO(Seat seat) 
        {
            return new SeatIncludedWithTheaterDTO()
            {
                Id = seat.Id,
                Row = seat.Row,
                SeatNumber = seat.SeatNumber,
            };
        }
    }
}
