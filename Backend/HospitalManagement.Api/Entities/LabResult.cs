namespace HospitalManagement.Api.Entities;

public class LabResult
{
    public int Id { get; set; }

    public required string UserId { get; set; }

    public required string UserEmail { get; set; }

    public required string Title { get; set; }

    public string ResultText { get; set; } = string.Empty;

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? FileType { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string CreatedByAdminId { get; set; } = default!;
}