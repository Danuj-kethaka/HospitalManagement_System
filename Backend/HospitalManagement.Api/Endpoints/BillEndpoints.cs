using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Api.Dtos;
using HospitalManagement.Api.Mapping;
using HospitalManagement.Api.Entities;

namespace HospitalManagement.Api.Endpoints;

public static class BillEndpoints
{
    public static RouteGroupBuilder MapBillEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("bills");

        // ADMIN CREATE BILL
        group.MapPost("/", async (
            CreateBill request,
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            ClaimsPrincipal user) =>
        {
            if (!user.IsInRole("Admin"))
                return Results.Forbid();

            var adminId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            var targetUser = await userManager.FindByIdAsync(request.UserId);

            if (targetUser is null)
                return Results.NotFound("User not found");

            var bill = request.ToEntity(targetUser.Email!, adminId!);

            db.Set<Bill>().Add(bill);

            await db.SaveChangesAsync();

            return Results.Ok(bill);

        }).RequireAuthorization();


        // USER GET OWN BILLS
        group.MapGet("/", async (
            ApplicationDbContext db,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Results.Unauthorized();

            var bills = await db.Set<Bill>()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return Results.Ok(bills);

        }).RequireAuthorization();


        // ADMIN GET ALL BILLS
        group.MapGet("/all", async (
            ApplicationDbContext db,
            ClaimsPrincipal user) =>
        {
            if (!user.IsInRole("Admin"))
                return Results.Forbid();

            var bills = await db.Set<Bill>()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Results.Ok(bills);

        }).RequireAuthorization();

        return group;
    }
}