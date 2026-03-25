using System;
using HospitalManagement.Api.Dtos;
using HospitalManagement.Api.Endpoints;
namespace HospitalManagement.Api.Mapping;

public static class AppointmentMapping
{
    public static Appointment ToEntity(this CreateAppointment appointment, string userId, string email )
    {
        return new Appointment()
        {
            DoctoreName = appointment.DoctoreName,
            DepartmentName = appointment.DepartmentName,
            DateTime = appointment.DateTime,
            UserId = userId,
            UserEmail = email
        };
    }


}
