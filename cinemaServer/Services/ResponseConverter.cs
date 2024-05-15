using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response;

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
                Theater = screening.Theater,
                Tickets = screening.Tickets,
                StartTime = screening.StartTime,
            };
        }
    }
}
