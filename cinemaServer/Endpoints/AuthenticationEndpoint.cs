using cinemaServer.Data;
using cinemaServer.Models.Authentication;
using cinemaServer.Models.User;
using cinemaServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class AuthenticationEndpoint
    {
        public static void AuthenticationEndpointConfiguration(this WebApplication app) 
        {
            var authGroup = app.MapGroup("auth");

            authGroup.MapPost("/signup", Register);
            authGroup.MapPost("/login", Authenticate);
        }

        public static async Task<IResult> Register(UserManager<ApplicationUser> userManager, [FromBody] RegistrationRequest request) 
        {
            if (!request.IsValid()) 
            {
                return TypedResults.BadRequest();
            }

            IdentityResult res = await userManager.CreateAsync(
                new ApplicationUser { 
                    UserName = request.Username,
                    Email = request.Email,
                    Role = ERole.User,
                }, request.Password!
            );

            if (res.Succeeded) 
            {
                request.Password = "";
                return TypedResults.Created("/", new { username = request.Username, email = request.Email, role = ERole.User});
            }
            return TypedResults.BadRequest(res.Errors.ToList());
        }

        public static async Task<IResult> Authenticate(UserManager<ApplicationUser> userManager, TokenService tokenService, DataContext dataContext, [FromBody] AuthRequest request) 
        {
            if (!request.IsValid()) 
            {
                return TypedResults.BadRequest("Invalid request.");
            }

            ApplicationUser? managedUser = await userManager.FindByEmailAsync(request.Email!);
            if (managedUser == null) 
            {
                return TypedResults.BadRequest("Invalid login email.");
            }

            bool validPassword = await userManager.CheckPasswordAsync(managedUser, request.Password!);
            if (!validPassword) 
            {
                return TypedResults.BadRequest("Invalid login credentials.");
            }

            ApplicationUser? dbUser = dataContext.Users.FirstOrDefault(u => u.Email == request.Email);
            if (dbUser == null) 
            {
                return TypedResults.Unauthorized();
            }

            string accessToken = tokenService.CreateToken(dbUser);
            await dataContext.SaveChangesAsync();

            return TypedResults.Ok(new AuthResponse 
            {
                Id = dbUser.Id,
                Username = dbUser.UserName,
                Email = dbUser.Email,
                Role = dbUser.Role,
                Token = accessToken,
            });
        }
    }
}
