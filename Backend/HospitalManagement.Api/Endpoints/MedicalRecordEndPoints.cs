using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Api.Dtos;
using HospitalManagement.Api.Mapping;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagement.Api.Endpoints;

public static class MedicalRecordEndpoints
{
    public static RouteGroupBuilder MapMedicalRecordEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("medical-records");

        // ADMIN: CREATE RECORD
        group.MapPost("/", async (
            CreateMedicalRecord request,
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

            var record = request.ToEntity(targetUser.Email!, adminId!);

            db.Set<MedicalRecord>().Add(record);
            await db.SaveChangesAsync();

            return Results.Ok(record);
        }).RequireAuthorization();


        // USER: GET OWN RECORDS
        group.MapGet("/", async (
            ApplicationDbContext db,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Results.Unauthorized();

            var records = await db.Set<MedicalRecord>()
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return Results.Ok(records);
        }).RequireAuthorization();


        // ADMIN: GET ALL RECORDS
        group.MapGet("/all", async (
            ApplicationDbContext db,
            ClaimsPrincipal user) =>
        {
            if (!user.IsInRole("Admin"))
                return Results.Forbid();

            var records = await db.Set<MedicalRecord>().ToListAsync();
            return Results.Ok(records);

        }).RequireAuthorization();

        return group;
    }
}