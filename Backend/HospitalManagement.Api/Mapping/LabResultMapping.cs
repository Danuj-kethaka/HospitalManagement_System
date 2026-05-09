namespace HospitalManagement.Api.Mapping;

using HospitalManagement.Api.Dtos;
using HospitalManagement.Api.Entities;

public static class LabResultMapping
{
    public static LabResult ToEntity(
        this CreateLabResult request,
        string userEmail,
        string adminId,
        string? fileName,
        string? filePath,
        string? fileType)
    {
        return new LabResult
        {
            UserId = request.UserId,
            UserEmail = userEmail,
            Title = request.Title,
            ResultText = request.ResultText,

            FileName = fileName,
            FilePath = filePath,
            FileType = fileType,

            Status = "Pending",
            CreatedByAdminId = adminId
        };
    }
}