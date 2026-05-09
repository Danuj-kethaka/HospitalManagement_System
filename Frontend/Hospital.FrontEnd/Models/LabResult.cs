namespace Hospital.FrontEnd.Models;

public class LabResult
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string ResultText { get; set; } = string.Empty;

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public string? FileType { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedAt { get; set; }
}