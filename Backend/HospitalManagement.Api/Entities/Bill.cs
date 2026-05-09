namespace HospitalManagement.Api.Entities;

public class Bill
{
    public int Id { get; set; }

    public required string UserId { get; set; }

    public required string UserEmail { get; set; }

    public required string Title { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string CreatedByAdminId { get; set; } = default!;
}