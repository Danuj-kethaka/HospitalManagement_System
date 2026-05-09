namespace HospitalManagement.Api.Endpoints;

public class MedicalRecord
{
    public int Id { get; set; }

    public required string UserId { get; set; }

    public required string UserEmail { get; set; }

    public required string Title { get; set; }    

    public required string Description { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public string CreatedByAdminId { get; set; } = default!;
}