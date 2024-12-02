using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceProvidersDirectory.Migrations
{
    /// <inheritdoc />
    public partial class addingpostcodefieldtohospital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Hospitals",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Hospitals");
        }
    }
}
