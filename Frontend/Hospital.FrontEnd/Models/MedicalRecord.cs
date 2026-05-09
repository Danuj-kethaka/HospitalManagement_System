using System;

namespace Hospital.FrontEnd.Models;

public class MedicalRecord
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}