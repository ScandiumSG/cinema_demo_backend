using cinemaServer.Models.PureModels;
using cinemaServer.Models.Request;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class ScreeningEndpoint
    {
        public static void ScreeningEndpointConfiguration(this WebApplication app) 
        {
            var screeningGroup = app.MapGroup("screening/");

            screeningGroup.MapGet("", GetAll);
            screeningGroup.MapGet("/{id}", GetSpecific);
            screeningGroup.MapPost("", PostScreening);
            screeningGroup.MapPut("", PutScreening);
            screeningGroup.MapDelete("", DeleteScreening);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public static async Task<IResult> GetAll(IRepository<Screening> repo) 
        {
            List<Screening> screenings = await repo.Get();
            if (screenings.Count == 0) {
                return TypedResults.NoContent();
            }

            Payload<List<Screening>> payload = new Payload<List<Screening>>(screenings);
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetSpecific(IRepository<Screening> repo, int id) 
        {
            Screening? screening = await repo.GetSpecific((object) id);
            if (screening == null)
            {
                return TypedResults.NotFound();
            }

            Payload<Screening> payload = new Payload<Screening>(screening);
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PostScreening(IRepository<Screening> repo, PostScreeningDTO postObject) 
        {
            Screening inputScreening = new Screening() 
            { 
                MovieId = postObject.MovieId,
                TheaterId = postObject.TheaterId,
                StartTime = postObject.StartTime,
            };

            Screening? savedScreening = await repo.Create(inputScreening);
            if (savedScreening == null) 
            {
                return TypedResults.BadRequest();
            }

            Payload<Screening> payload = new Payload<Screening>(savedScreening);
            return TypedResults.Created($"/{savedScreening.Id}", payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PutScreening(IRepository<Screening> repo, PutScreeningDTO putObject) 
        {
            Screening inputScreening = new Screening() 
            { 
                Id = putObject.Id,
                MovieId = putObject.MovieId,
                TheaterId   = putObject.TheaterId,
                StartTime = putObject.StartTime,
            };
            Screening? updatedScreening = await repo.Update(inputScreening);

            if (updatedScreening == null) 
            {
                return TypedResults.BadRequest();
            }

            Payload<Screening> payload = new Payload<Screening>(updatedScreening);
            return TypedResults.Created($"/{updatedScreening.Id}", payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> DeleteScreening(IRepository<Screening> repo, int screeningId) 
        {
            Screening? deletedScreening = await repo.Delete(screeningId);

            if (deletedScreening == null) 
            {
                return TypedResults.NotFound();
            }

            Payload<Screening> payload = new Payload<Screening>(deletedScreening);
            return TypedResults.Ok(payload);
        }
    }
}
