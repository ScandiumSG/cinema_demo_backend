using cinemaServer.Models.PureModels;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Models.Response.TicketResponse;
using cinemaServer.Repository;
using cinemaServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class TicketEndpoint
    {
        public static void TicketEndpointConfiguration(this WebApplication app) 
        {
            RouteGroupBuilder ticketGroup = app.MapGroup("/tickets");

            ticketGroup.MapGet("/screening/{screeningId}", GetTicketsForSpecificScreening);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public static async Task<IResult> GetTicketsForSpecificScreening(IRepository<Ticket> repo, int screeningId) 
        {
            IEnumerable<Ticket> tickets = await (repo as TicketRepository)!.GetSpecificByScreening(screeningId);

            if (tickets.Count() == 0) 
            {
                return TypedResults.NoContent();
            }

            IEnumerable<TicketInScreeningDTO> convTickets = tickets.Select((t) => ResponseConverter.ConvertTicketToScreeningDTO(t)).ToList();
            Payload<IEnumerable<TicketInScreeningDTO>> payload = new Payload<IEnumerable<TicketInScreeningDTO>>(convTickets);

            return TypedResults.Ok(payload);
        }
    }
}
