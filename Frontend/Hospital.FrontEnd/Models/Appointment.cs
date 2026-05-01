using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Appointment
{
    public int Id { get; set; }
    public string? DoctoreName { get; set; }        
    public string? DepartmentName { get; set; }
    public DateTime DateTime { get; set; }          
    public string? UserId { get; set; }
    public string? UserEmail { get; set; }
    public string? UserName { get; set; }           

}