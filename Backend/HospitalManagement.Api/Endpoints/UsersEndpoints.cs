using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Api.Endpoints;

public static class UsersEndpoints
{
    public static RouteGroupBuilder MapUserEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("users");

        //users/me
        group.MapGet("/me", async (ApplicationDbContext dbContext, ClaimsPrincipal claimsPrincipal,UserManager<ApplicationUser> userManager) =>
        {
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Results.Unauthorized();
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Results.NotFound("User not found");
            }

            var roles = await userManager.GetRolesAsync(user);
                
            return Results.Ok(new
            {
                user.Id,
                user.Email,
                user.Initials,
                user.EnableNotification,
                Role = roles.FirstOrDefault()
                
            });
        }).RequireAuthorization();

        //get all users
        group.MapGet("/all", async ( UserManager<ApplicationUser> userManager,ClaimsPrincipal user) =>
            {
                if (!user.IsInRole("Admin"))
                {
                    return Results.Forbid();
                }

                var users = await userManager.Users.ToListAsync();

                var result = new List<object>();

                foreach (var u in users)
                {
                    var roles = await userManager.GetRolesAsync(u);

                    result.Add(new
                    {
                        u.Id,
                        u.Email,
                        u.Initials,
                        u.EnableNotification,
                        Role = roles.FirstOrDefault()
                    });
                }
                return Results.Ok(result);
        }).RequireAuthorization();


        return group;
    }
}