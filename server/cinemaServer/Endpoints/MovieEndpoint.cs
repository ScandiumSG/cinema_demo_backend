using cinemaServer.Models.PureModels;
using cinemaServer.Models.Request.Post;
using cinemaServer.Models.Request.Put;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class MovieEndpoint
    {
        public static void MovieEndpointConfiguration(this WebApplication app)
        {
            var screeningGroup = app.MapGroup("movie");

            screeningGroup.MapGet("/", Get);
            screeningGroup.MapGet("/{id}", GetSpecific);
            screeningGroup.MapPost("/", PostScreening);
            screeningGroup.MapPut("/", PutScreening);
            screeningGroup.MapDelete("/", DeleteScreening);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public static async Task<IResult> Get(IRepository<Movie> repo, int limit = 50)
        {
            List<Movie> movies = await repo.Get(limit);
            if (movies.Count == 0)
            {
                return TypedResults.NoContent();
            }

            Payload<List<Movie>> payload = new Payload<List<Movie>>(movies.ToList());
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetSpecific(IRepository<Movie> repo, int id)
        {
            Movie? movie = await repo.GetSpecific((object)id);
            if (movie == null)
            {
                return TypedResults.NotFound();
            }

            Payload<Movie> payload = new Payload<Movie>(movie);
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PostScreening(IRepository<Movie> repo, PostMovieDTO postObject)
        {
            Movie inputMovie = new Movie()
            {
                Title = postObject.Title,
                Description = postObject.Description,
                Runtime = postObject.Runtime,
                Year = postObject.Year,
                Rating = postObject.Rating
            };

            Movie? savedMovie = await repo.Create(inputMovie);
            if (savedMovie == null)
            {
                return TypedResults.BadRequest();
            }

            Payload<Movie> payload = new Payload<Movie>(savedMovie);
            return TypedResults.Created($"/{savedMovie.Id}", payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> PutScreening(IRepository<Movie> repo, PutMovieDTO putObject)
        {
            Movie? dbMovie = await repo.GetSpecific(putObject.Id);
            if (dbMovie == null) 
            {
                return TypedResults.NotFound();
            }

            dbMovie.Title = putObject.Title ?? dbMovie.Title;
            dbMovie.Description = putObject.Description ?? dbMovie.Description;
            dbMovie.Runtime = putObject.Runtime ?? dbMovie.Runtime;
            dbMovie.Year = putObject.Year ?? dbMovie.Year;

            Movie? updatedMovie = await repo.Update(dbMovie);

            if (updatedMovie == null)
            {
                return TypedResults.BadRequest();
            }

            Payload<Movie> payload = new Payload<Movie>(updatedMovie);
            return TypedResults.Created($"/{updatedMovie.Id}", payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteScreening(IRepository<Movie> repo, int movieId)
        {
            Movie? deletedMovie = await repo.Delete(movieId);

            if (deletedMovie == null)
            {
                return TypedResults.NotFound();
            }

            Payload<Movie> payload = new Payload<Movie>(deletedMovie);
            return TypedResults.Ok(payload);
        }
    }
}
