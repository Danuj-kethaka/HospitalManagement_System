using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Api.Dtos;
using HospitalManagement.Api.Entities;
using HospitalManagement.Api.Mapping;

namespace HospitalManagement.Api.Endpoints;

public static class LabResultEndpoints
{
    public static RouteGroupBuilder MapLabResultEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("labresults");

        // ADMIN CREATE LAB RESULT (WITH OPTIONAL FILE)
        group.MapPost("/", async (
            [FromForm] CreateLabResult request,
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            ClaimsPrincipal user,
            IWebHostEnvironment env) =>
        {
            if (!user.IsInRole("Admin"))
                return Results.Forbid();

            var adminId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            var targetUser = await userManager.FindByIdAsync(request.UserId);

            if (targetUser is null)
                return Results.NotFound("User not found");

            string? fileName = null;
            string? filePath = null;
            string? fileType = null;

            // FILE UPLOAD (CLEAN FIXED VERSION)
            if (request.File is not null)
            {
                var webRoot = env.WebRootPath 
                              ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                var uploadsFolder = Path.Combine(webRoot, "uploads", "labresults");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";

                var fullPath = Path.Combine(uploadsFolder, fileName);

                var relativePath = Path.Combine("uploads", "labresults", fileName)
                                    .Replace("\\", "/");

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                fileType = request.File.ContentType;
                filePath = relativePath;
            }

            var labResult = request.ToEntity(
                targetUser.Email!,
                adminId!,
                fileName,
                filePath,
                fileType
            );

            db.Set<LabResult>().Add(labResult);
            await db.SaveChangesAsync();

            return Results.Ok(labResult);

        })
        .RequireAuthorization()
        .DisableAntiforgery();

        // USER GET OWN LAB RESULTS
        group.MapGet("/", async (
            ApplicationDbContext db,
            ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Results.Unauthorized();

            var results = await db.Set<LabResult>()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Results.Ok(results);

        }).RequireAuthorization();

        // ADMIN GET ALL LAB RESULTS
        group.MapGet("/all", async (
            ApplicationDbContext db,
            ClaimsPrincipal user) =>
        {
            if (!user.IsInRole("Admin"))
                return Results.Forbid();

            var results = await db.Set<LabResult>()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return Results.Ok(results);

        }).RequireAuthorization();

        return group;
    }
}