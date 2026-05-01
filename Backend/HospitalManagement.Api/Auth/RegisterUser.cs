using System;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagement.Api.Auth;

public static class RegisterUser
{
    public record Request(string Email, string Initials, string Password, bool EnableNotifications = false);
    
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("register",async(Request request,ApplicationDbContext dbContext, UserManager<ApplicationUser> UserManager) =>
        {
            using var transaction = await dbContext.Database.BeginTransactionAsync();
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                Initials = request.Initials,
                EnableNotification = request.EnableNotifications
            };
              IdentityResult identityResult = await UserManager.CreateAsync(user, request.Password);
              if(!identityResult.Succeeded)
            {
                return Results.BadRequest(identityResult.Errors);
            }

              IdentityResult AddToRoleResult =  await UserManager.AddToRoleAsync(user,Roles.Admin);
              if(!AddToRoleResult.Succeeded)
            {
                return Results.BadRequest(AddToRoleResult.Errors);
            }

            await transaction.CommitAsync();

            return Results.Ok(user);
        });
    }
}
