using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dateTime",
                schema: "identity",
                table: "appointments",
                newName: "DateTime");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "identity",
                table: "appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "identity",
                table: "appointments");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                schema: "identity",
                table: "appointments",
                newName: "dateTime");
        }
    }
}
