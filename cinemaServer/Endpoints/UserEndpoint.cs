﻿using cinemaServer.Models.Request.Put;
using cinemaServer.Models.Response.Payload;
using cinemaServer.Models.User;
using cinemaServer.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cinemaServer.Endpoints
{
    public static class UserEndpoint
    {
        public static void UserEndpointConfiguration(this WebApplication app)
        {
            var userGroup = app.MapGroup("/user");

            userGroup.MapPut("/change", UpdateUserInfo);
            userGroup.MapPut("/changepw", UpdateUserPassword);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> UpdateUserInfo(IRepository<ApplicationUser> repo, UserManager<ApplicationUser> userManager, PutUserDTO putUser)
        {
            ApplicationUser? dbUser = await repo.GetSpecific(putUser.Id);
            if (dbUser == null)
            {
                return TypedResults.NotFound();
            }

            if (await userManager.IsLockedOutAsync(dbUser))
            {
                return TypedResults.BadRequest("Too many failed attempts, try again later.");
            }



            bool validPassword = await userManager.CheckPasswordAsync(dbUser, putUser.Password);
            if (!validPassword)
            {
                await userManager.AccessFailedAsync(dbUser);
                return TypedResults.BadRequest("Invalid password");
            }
            else
            {
                await userManager.ResetAccessFailedCountAsync(dbUser);
            }

            dbUser.Email = putUser.Email ?? dbUser.Email;
            dbUser.UserName = putUser.Username ?? dbUser.UserName;

            ApplicationUser? updatedUser = await repo.Update(dbUser);

            Payload<ApplicationUser> payload = new Payload<ApplicationUser>(updatedUser!);
            return TypedResults.Created("/", payload);
        }

        public static async Task<IResult> UpdateUserPassword(IRepository<ApplicationUser> repo, UserManager<ApplicationUser> userManager, PutUserPwDTO pwRequest) 
        {
            ApplicationUser? dbUser = await repo.GetSpecific(pwRequest.Id);
            if (dbUser == null) 
            {
                return TypedResults.NotFound();
            }

            bool passwordMatch = await userManager.CheckPasswordAsync(dbUser, pwRequest.OldPassword);
            if (!passwordMatch) 
            {
                return TypedResults.BadRequest();
            }

            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            dbUser.PasswordHash = hasher.HashPassword(dbUser, pwRequest.NewPassword);
            IdentityResult updatedUser = await userManager.UpdateAsync(dbUser);
            if (updatedUser.Succeeded)
            {
                return TypedResults.Created("/", dbUser);
            } else 
            {
                return TypedResults.BadRequest();
            }
        }
    }
}
