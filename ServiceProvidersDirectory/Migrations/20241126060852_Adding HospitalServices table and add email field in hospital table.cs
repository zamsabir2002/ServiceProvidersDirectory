using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceProvidersDirectory.Migrations
{
    /// <inheritdoc />
    public partial class AddingHospitalServicestableandaddemailfieldinhospitaltable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Hospitals");
        }
    }
}
