namespace HospitalManagement.Api.Mapping;
using System;
using HospitalManagement.Api.Dtos;
using HospitalManagement.Api.Endpoints;

public static class MedicalRecordMapping
{
    public static MedicalRecord ToEntity(this CreateMedicalRecord request, string userEmail, string adminId)
    {
        return new MedicalRecord
        {
            UserId = request.UserId,
            UserEmail = userEmail,
            Title = request.Title,
            Description = request.Description,
            CreatedByAdminId = adminId
        };
    }
}