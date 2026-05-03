using System;

namespace HospitalManagement.Api.Endpoints;

public class Appointment
{
   public int Id {get; set;}

   public required string DoctoreName {get; set;}

   public required string DepartmentName {get; set;}

   public required string UserId {get;set;}

   public required string UserEmail {get; set;}

   public DateTime DateTime {get; set;}

   public string Status { get; set; } = "Pending";
}
