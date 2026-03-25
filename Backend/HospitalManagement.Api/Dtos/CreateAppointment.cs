using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Api.Dtos;

public  record class CreateAppointment
(
  [Required] string DoctoreName,

  [Required] string DepartmentName,

  [Required] DateTime DateTime

);
