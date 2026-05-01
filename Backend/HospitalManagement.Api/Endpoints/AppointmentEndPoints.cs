using System;
using HospitalManagement.Api.Dtos;
namespace HospitalManagement.Api.Endpoints;
using System.Threading.Tasks;
using HospitalManagement.Api.Mapping;
using Microsoft.EntityFrameworkCore;
using HospitalManagement.Api.Dtos;
using System.Security.Claims;

public static class AppointmentEndPoints
{
    public static RouteGroupBuilder MapAppointmentEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("appointments");

        //create appointments
        group.MapPost("/",async(CreateAppointment newAppointment, ApplicationDbContext dbContext, ClaimsPrincipal user)=>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = user.FindFirstValue(ClaimTypes.Email);

            if(userId is null || email is null)
            {
                return Results.Unauthorized();
            }

            Appointment appointment = newAppointment.ToEntity(userId,email);
            dbContext.appointments.Add(appointment);
            await dbContext.SaveChangesAsync();
            return Results.Ok(appointment);        
        }).RequireAuthorization();


        //get appointments
        group.MapGet("/", async (ApplicationDbContext dbContext, ClaimsPrincipal user) =>
        {
            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                return Results.Unauthorized(); 
            }
               
            var appointments = await dbContext.appointments
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return Results.Ok(appointments);
        }).RequireAuthorization();
        
        
        group.MapGet("/all", async (ApplicationDbContext dbContext, ClaimsPrincipal user) =>
        {
            
            if (!user.IsInRole("Admin"))
            {
                return Results.Forbid();   
            }

            var appointments = await dbContext.appointments.ToListAsync();

            return Results.Ok(appointments);
        }).RequireAuthorization();

      



        return group;



    }
}
