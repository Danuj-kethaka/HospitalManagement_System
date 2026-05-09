using HospitalManagement.Api.Dtos;
using HospitalManagement.Api.Entities;

namespace HospitalManagement.Api.Mapping;

public static class BillMapping
{
    public static Bill ToEntity(
        this CreateBill request,
        string userEmail,
        string adminId)
    {
        return new Bill
        {
            UserId = request.UserId,
            UserEmail = userEmail,
            Title = request.Title,
            Amount = request.Amount,
            Status = "Pending",
            CreatedByAdminId = adminId
        };
    }
}