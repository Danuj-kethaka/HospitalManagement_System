namespace HospitalManagement.Api.Dtos;

public class CreateLabResult
{
    public string UserId { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string ResultText { get; set; } = string.Empty;

    public IFormFile? File { get; set; }
}