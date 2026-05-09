namespace HospitalManagement.Api.Dtos;

public record class CreateBill
(
    string UserId,
    string Title,
    decimal Amount,
    string Status
);