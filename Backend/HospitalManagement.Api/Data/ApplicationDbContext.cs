using HospitalManagement.Api.Endpoints;
using HospitalManagement.Api.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Appointment> appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<LabResult> LabResults { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.EnableNotification).HasDefaultValue(true);
            entity.Property(e => e.Initials).HasMaxLength(5);
        });

        builder.HasDefaultSchema("identity");

    }
}
