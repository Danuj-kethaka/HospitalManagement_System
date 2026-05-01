// Models/CreateAppointment.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class CreateAppointment
{
     [Required(ErrorMessage = "The DoctoreName is Required")]
    public string DoctoreName { get; set; }

    [Required(ErrorMessage = "The DepartmentName is Required")]
    public  string DepartmentName { get; set; } 

    [Required]
    public DateTime DateTime { get; set; }

}