using cinemaServer.Models.PureModels;
using cinemaServer.Models.Request.Post;
using cinemaServer.Models.Request.Put;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Models.Response.ScreeningRespose;
using cinemaServer.Repository;
using cinemaServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class ScreeningEndpoint
    {
        public static void ScreeningEndpointConfiguration(this WebApplication app) 
        {
            var screeningGroup = app.MapGroup("screening");

            screeningGroup.MapGet("/", GetAll);
            screeningGroup.MapGet("/{screeningId}-{movieId}", GetSpecific);
            screeningGroup.MapPost("/", PostScreening);
            screeningGroup.MapPut("/", PutScreening);
            screeningGroup.MapDelete("/{screeningId}-{movieId}", DeleteScreening);
            screeningGroup.MapGet("/upcoming/{limitDate}", GetUpcomingScreenings);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public static async Task<IResult> GetAll(ICompUpcomingRepository<Screening> repo, int limit = 10) 
        {
            List<Screening> screenings = await repo.Get(limit);
            if (screenings.Count == 0) {
                return TypedResults.NoContent();
            }

            Payload<List<ScreeningResponseDTO>> payload = new Payload<List<ScreeningResponseDTO>>(
                screenings.Select(s => ResponseConverter.ConvertScreening(s)).ToList()
            );
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetSpecific(ICompUpcomingRepository<Screening> repo, int screeningId, int movieId) 
        {
            Screening? screening = await repo.GetSpecific(screeningId, movieId);
            if (screening == null)
            {
                return TypedResults.NotFound();
            }

            Payload<ScreeningResponseDTO> payload = new Payload<ScreeningResponseDTO>(
                ResponseConverter.ConvertScreening(screening)
            );
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> PostScreening(ICompUpcomingRepository<Screening> repo, IRepository<Movie> movieRepo, IRepository<Theater> theaterRepo, PostScreeningDTO postObject) 
        {
            Movie? dbMovie = await movieRepo.GetSpecific(postObject.MovieId);
            if (dbMovie == null) 
            {
                return TypedResults.NotFound();
            }

            Theater? dbTheater = await theaterRepo.GetSpecific(postObject.TheaterId);
            if (dbTheater == null)
            {
                return TypedResults.NotFound();
            }

            Screening inputScreening = new Screening() 
            { 
                MovieId = postObject.MovieId,
                Movie = dbMovie,
                TheaterId = postObject.TheaterId,
                Theater = dbTheater,
                StartTime = postObject.StartTime,
            };

            Screening? savedScreening = await repo.Create(inputScreening);
            if (savedScreening == null) 
            {
                return TypedResults.BadRequest();
            }

            Payload<ScreeningResponseDTO> payload = new Payload<ScreeningResponseDTO>(
                ResponseConverter.ConvertScreening(savedScreening)
            );
            return TypedResults.Created($"/{savedScreening.Id}", payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PutScreening(ICompUpcomingRepository<Screening> repo, IRepository<Movie> movieRepo, IRepository<Theater> theaterRepo, PutScreeningDTO putObject) 
        {
            Movie? dbMovie = await movieRepo.GetSpecific(putObject.MovieId);
            if (dbMovie == null)
            {
                return TypedResults.NotFound();
            }

            Theater? dbTheater = await theaterRepo.GetSpecific(putObject.TheaterId);
            if (dbTheater == null)
            {
                return TypedResults.NotFound();
            }

            Screening inputScreening = new Screening() 
            { 
                Id = putObject.Id,
                MovieId = putObject.MovieId,
                Movie = dbMovie,
                TheaterId   = putObject.TheaterId,
                Theater = dbTheater,
                StartTime = putObject.StartTime,
            };
            Screening? updatedScreening = await repo.Update(inputScreening);

            if (updatedScreening == null) 
            {
                return TypedResults.BadRequest();
            }

            Payload<ScreeningResponseDTO> payload = new Payload<ScreeningResponseDTO>(
                ResponseConverter.ConvertScreening(updatedScreening)
            );
            return TypedResults.Created($"/{updatedScreening.Id}", payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteScreening(ICompUpcomingRepository<Screening> repo, int screeningId, int movieId) 
        {
            Screening? deletedScreening = await repo.Delete(screeningId, movieId);

            if (deletedScreening == null) 
            {
                return TypedResults.NotFound();
            }

            Payload<ScreeningResponseDTO> payload = new Payload<ScreeningResponseDTO>(
                ResponseConverter.ConvertScreening(deletedScreening)
            );
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public static async Task<IResult> GetUpcomingScreenings(ICompUpcomingRepository<Screening> repo, DateTime limitDate, int limit = 10)
        {
            IEnumerable<Screening> upcomingScreenings = await repo.GetUpcoming(limit, limitDate);

            if (!upcomingScreenings.Any()) 
            {
                return TypedResults.NoContent();
            }

            IEnumerable<ScreeningResponseShortenedDTO> screeningsShortened = upcomingScreenings.Select((s) => ResponseConverter.ConvertShortenedScreening(s)).ToList();

            Payload<IEnumerable<ScreeningResponseShortenedDTO>> payload = new Payload<IEnumerable<ScreeningResponseShortenedDTO>>(screeningsShortened);

            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public static async Task<IResult> GetUpcomingScreeningsOfMovie(ICompUpcomingRepository<Screening> repo, int movieId, DateTime limitDate, int limit = 10) 
        {
            IEnumerable<Screening> upcomingScreenings = await repo.GetSpecificUpcoming(movieId, limit, limitDate);


            if (!upcomingScreenings.Any())
            {
                return TypedResults.NoContent();
            }

            IEnumerable<ScreeningResponseShortenedDTO> screeningsShortened = upcomingScreenings.Select((s) => ResponseConverter.ConvertShortenedScreening(s)).ToList();

            Payload<IEnumerable<ScreeningResponseShortenedDTO>> payload = new Payload<IEnumerable<ScreeningResponseShortenedDTO>>(screeningsShortened);

            return TypedResults.Ok(payload);
        }
    }
}
