namespace HospitalManagement.Api.Dtos;

public record class CreateMedicalRecord
(
    string UserId,
    string Title,
    string Description
);