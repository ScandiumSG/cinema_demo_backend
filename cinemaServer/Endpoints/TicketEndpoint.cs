using cinemaServer.Models.PureModels;
using cinemaServer.Models.Request.Post;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Models.Response.TicketResponse;
using cinemaServer.Models.User;
using cinemaServer.Repository;
using cinemaServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class TicketEndpoint
    {
        public static void TicketEndpointConfiguration(this WebApplication app) 
        {
            var TicketGroup = app.MapGroup("tickets");

            //TicketGroup.MapGet("/", GetTickets);
            TicketGroup.MapGet("/{id}", GetSpecificTicket);
            TicketGroup.MapGet("/refetch/{screeningId}/{movieId}", GetTicketsForScreening);
            TicketGroup.MapPost("/", CreateTicket);
            //TicketGroup.MapPut("/", UpdateTicket);
            //TicketGroup.MapDelete("/{id}", DeleteTicket);
            TicketGroup.MapGet("/purchased", GetPurchasedTickets);
            TicketGroup.MapGet("/types", GetTicketTypes);
        }

        public static async Task<IResult> GetTickets(IRepository<Ticket> repo) 
        {
            List<Ticket> tickets = await repo.Get(100);

            List<TicketDTO> ticketDTOs = ResponseConverter.ConvertTicketToDTO(tickets);
            Payload<List<TicketDTO>> payload = new Payload<List<TicketDTO>>(ticketDTOs);

            return TypedResults.Ok(payload);
        }

        public static async Task<IResult> GetSpecificTicket(IRepository<Ticket> repo, int id)
        {
            Ticket? ticket = await repo.GetSpecific(id);
            if (ticket == null)
            {
                return TypedResults.NotFound();
            }

            List<TicketDTO> ticketDTO = ResponseConverter.ConvertTicketToDTO(new List<Ticket>{ticket});
            Payload<TicketDTO> payload = new Payload<TicketDTO>(ticketDTO.First());

            return TypedResults.Ok(payload);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> CreateTicket(
            IRepository<Ticket> repo, 
            ICompUpcomingRepository<Screening> screeningRepo, 
            IRepository<Seat> seatRepo, 
            IRepository<ApplicationUser> userRepo, 
            IRepository<TicketType> ticketTypeRepo,
            PostTicketDTO postTicket) 
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

            IEnumerable<Seat> seatForTheater = await (seatRepo as SeatRepository)!.GetSeatsForTheater(associatedScreening.TheaterId);
            IList<TicketType> allTicketTypes = await ticketTypeRepo.Get(null);

            List<Ticket> queuedTickets = new List<Ticket>();
            for (int i = 0; i < postTicket.SeatId.Count; i++)
            {
                Seat? seat = seatForTheater.Where((s) => s.Id == postTicket.SeatId.ElementAt(i)).FirstOrDefault();
                TicketType? ticketType = allTicketTypes.Where((t) => t.Id == postTicket.TicketTypeId.ElementAt(i)).FirstOrDefault();

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
                    TicketType = ticketType,
                    TicketTypeId = ticketType!.Id,
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public static async Task<IResult> GetTicketsForScreening(IRepository<Ticket> repo, int screeningId, int movieId) 
        {
            List<Ticket> tickets = await (repo as TicketRepository)!.GetTicketsForScreening(screeningId, movieId);
            if (tickets.Count() == 0) 
            {
                return TypedResults.NoContent();
            }

            List<TicketInScreeningDTO> convertedTickets = tickets.Select((ticket) => ResponseConverter.ConvertTicketToScreeningDTO(ticket)).ToList();
            Payload<List<TicketInScreeningDTO>> payload = new Payload<List<TicketInScreeningDTO>>(convertedTickets);

            return TypedResults.Ok(payload);
        }

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPurchasedTickets(IRepository<Ticket> repo, HttpContext httpContext, bool showPrevious = false) 
        {
            DateTime currentTime = DateTime.Now;
            var claimedUser = httpContext.User;
            if (!(claimedUser.Identity != null && claimedUser.Identity.IsAuthenticated)) 
            {
                return TypedResults.Unauthorized();
            }

            string? userId = claimedUser.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
            if (userId == null) 
            {
                return TypedResults.NotFound();
            }

            List<Ticket> dbTickets = await (repo as TicketRepository)!.GetTicketsForUser(userId);

            if (!showPrevious) 
            {
                dbTickets = dbTickets.Where((t) => t.Screening?.StartTime.CompareTo(currentTime) > 0).ToList();
            }

            List<TicketDTO> convertedTickets = ResponseConverter.ConvertTicketToDTO(dbTickets);
            Payload<List<TicketDTO>> payload = new Payload<List<TicketDTO>>(convertedTickets);

            return TypedResults.Ok(payload);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public static async Task<IResult> GetTicketTypes(IRepository<TicketType> repo) 
        {
            List<TicketType> dbTypes = await repo.Get(null);

            Payload<List<TicketType>> payload = new Payload<List<TicketType>>(dbTypes);

            return TypedResults.Ok(payload);
        }
    }
}
