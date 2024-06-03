using cinemaServer.Models.PureModels;
using cinemaServer.Models.Request.Post;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Models.Response.TicketResponse;
using cinemaServer.Models.User;
using cinemaServer.Repository;
using cinemaServer.Services;

namespace cinemaServer.Endpoints
{
    public static class TicketEndpoint
    {
        public static void TicketEndpointConfiguration(this WebApplication app) 
        {
            var TicketGroup = app.MapGroup("/tickets/");

            //TicketGroup.MapGet("/", GetTickets);
            TicketGroup.MapGet("/{id}", GetSpecificTicket);
            TicketGroup.MapPost("/", CreateTicket);
            //TicketGroup.MapPut("/", UpdateTicket);
            //TicketGroup.MapDelete("/{id}", DeleteTicket);
        }

        public static async Task<IResult> GetTickets(IRepository<Ticket> repo) 
        {
            IEnumerable<Ticket> tickets = await repo.Get(100);

            return TypedResults.Ok(tickets);
        }

        public static async Task<IResult> GetSpecificTicket(IRepository<Ticket> repo, int id) 
        {
            Ticket? ticket = await repo.GetSpecific(id);
            if (ticket == null) 
            {
                return TypedResults.NotFound();
            }

            var payload = ticket;

            return TypedResults.Ok(payload);
        }

        public static async Task<IResult> CreateTicket(IRepository<Ticket> repo, ICompUpcomingRepository<Screening> screeningRepo, IRepository<ApplicationUser> userRepo, PostTicketDTO postTicket) 
        {
            Screening? associatedScreening = await screeningRepo.GetSpecific(postTicket.ScreeningId, postTicket.MovieId);
            if (associatedScreening == null) 
            {
                return TypedResults.NotFound($"No screening of provided screening id ({postTicket.ScreeningId}) and movie id ({postTicket.MovieId}) found.");
            }

            ApplicationUser? user = await userRepo.GetSpecific(postTicket.CustomerId);
            if (user == null) 
            {
                return TypedResults.NotFound("User id provided not found.");
            }

            List<Ticket> queuedTickets = new List<Ticket>();
            foreach (int seatId in postTicket.SeatId) 
            { 
                Seat? seat = associatedScreening.Theater!.Seats.Where((s) => s.Id == seatId).FirstOrDefault();

                Ticket constructedTicket = new Ticket()
                {
                    Screening = associatedScreening,
                    ScreeningId = associatedScreening.Id,
                    MovieId = associatedScreening.MovieId,
                    TheaterId = associatedScreening.TheaterId,
                    CustomerId = user!.Id,
                    Customer = user,
                    Seat = seat,
                    SeatId = seat!.Id,
                };
                queuedTickets.Add(constructedTicket);
            }

            Tuple<int, List<Ticket>> postedTicket = await repo.CreateMultiple(queuedTickets);
            if (postedTicket.Item1 != queuedTickets.Count())
            {
                return TypedResults.BadRequest("Could not save tickets to database.");
            }

            List<TicketDTO> ticketOut = ResponseConverter.ConvertTicketToDTO(postedTicket.Item2);
            Payload<List<TicketDTO>> payload = new Payload<List<TicketDTO>>(ticketOut);

            return TypedResults.Ok(payload);
        }
    }
}
