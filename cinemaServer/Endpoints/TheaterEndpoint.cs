using cinemaServer.Models.PureModels;
using cinemaServer.Models.Request.Post;
using cinemaServer.Models.Request.Put;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Repository;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class TheaterEndpoint
    {
        public static void TheaterEndpointConfiguration(this WebApplication app) 
        {
            RouteGroupBuilder theaterGroup = app.MapGroup("location");

            theaterGroup.MapGet("/", Get);
            theaterGroup.MapGet("/{id}", GetSpecific);
            theaterGroup.MapPost("/", Create);
            theaterGroup.MapPut("/", Update);
            theaterGroup.MapDelete("/", Delete);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public static async Task<IResult> Get(IRepository<Theater> repo, int limit = 20) 
        {
            List<Theater>? theaters = await repo.Get(limit);
            if (theaters.Count == 0) 
            {
                return TypedResults.NoContent();
            }

            Payload<List<Theater>> payload = new Payload<List<Theater>>(theaters);
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetSpecific(IRepository<Theater> repo, int id) 
        {
            Theater? theater = await repo.GetSpecific(id);
            if (theater == null) 
            {
                return TypedResults.NotFound();
            }

            Payload<Theater> payload = new Payload<Theater>(theater);
            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> Create(IRepository<Theater> repo, PostTheaterDTO postObject) 
        {
            Theater theaterObject = new Theater()
            {
                Capacity = postObject.Capacity,
                Name = postObject.Name,
            };

            Tuple<int, Theater> savedTheater = await repo.Create(theaterObject);

            if (savedTheater.Item1 != 1) 
            {
                return TypedResults.BadRequest();
            }

            Payload<Theater> payload = new Payload<Theater>(savedTheater.Item2);
            return TypedResults.Created($"/{savedTheater.Item2.Id}", payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Update(IRepository<Theater> repo, PutTheaterDTO putObject) 
        {
            Theater? dbTheater = await repo.GetSpecific(putObject.Id);

            if (dbTheater == null) 
            {
                return TypedResults.NotFound();
            }

            dbTheater.Capacity = putObject.Capacity ?? dbTheater.Capacity;
            dbTheater.Name = putObject.Name ?? dbTheater.Name;

            Theater? savedTheater = await repo.Update(dbTheater);

            if (savedTheater == null) 
            {
                return TypedResults.BadRequest();
            }

            Payload<Theater> payload = new Payload<Theater>(savedTheater);
            return TypedResults.Created($"/{savedTheater.Id}", payload);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> Delete(IRepository<Theater> repo, int id) 
        {
            Theater? deleteTheater = await repo.Delete(id); ;
            if (deleteTheater == null)
            { 
                return TypedResults.NotFound();
            }

            Payload<Theater> payload = new Payload<Theater>(deleteTheater);
            return TypedResults.Ok(payload);
        }
    }
}
