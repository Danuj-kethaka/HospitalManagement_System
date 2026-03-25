using System;

namespace HospitalManagement.Api.Dtos;

public record class AppointmentDetails
(
   int Id,
   string DoctoreName,
   string DepartmentName,
   DateTime DateTime,
   string UserId,
   string UserEmail
);
