using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class CuratedContentEndpoint
    {
        public static void CurratedContentConfiguration(this WebApplication app) 
        {
            app.MapGet("/rel/highlight", GetHighlightedMovie);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetHighlightedMovie(CurationRepository repo) 
        {
            Movie highlightedMovie = await repo.GetHighlightedMovie();


            Payload<Movie> payload = new Payload<Movie>(highlightedMovie);

            return TypedResults.Ok(payload);
        }
    }
}
