using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServiceProvidersDirectory.Migrations
{
    /// <inheritdoc />
    public partial class Initial_create_and_models_from_erd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserUpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HospitalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_UserCreatedById",
                        column: x => x.UserCreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Users_UserUpdatedById",
                        column: x => x.UserUpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceProviders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProviders_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceProviders_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Services_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceProviderATs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SPId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    RequestedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceProviderATs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceProviderATs_RequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceProviderATs_ServiceProviders_SPId",
                        column: x => x.SPId,
                        principalTable: "ServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceProviderATs_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceProviderATs_Users_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceATs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    RequestedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceATs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceATs_RequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceATs_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceATs_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceATs_Users_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SPServiceReferrals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SPId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPServiceReferrals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SPServiceReferrals_ServiceProviders_SPId",
                        column: x => x.SPId,
                        principalTable: "ServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SPServiceReferrals_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SPServiceReferrals_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SPServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SPId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HowToRefer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SPServices_ServiceProviders_ServiceProviderId",
                        column: x => x.ServiceProviderId,
                        principalTable: "ServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SPServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SPServices_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SPServiceReferralATs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SPId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SPServiceReferralId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    RequestedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPServiceReferralATs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SPServiceReferralATs_RequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SPServiceReferralATs_SPServiceReferrals_SPServiceReferralId",
                        column: x => x.SPServiceReferralId,
                        principalTable: "SPServiceReferrals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SPServiceReferralATs_ServiceProviders_SPId",
                        column: x => x.SPId,
                        principalTable: "ServiceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SPServiceReferralATs_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SPServiceReferralATs_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SPServiceReferralATs_Users_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SPServiceATs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SPServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SPId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SPName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HowToRefer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    RequestTypeId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    RequestedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApprovedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPServiceATs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SPServiceATs_RequestTypes_RequestTypeId",
                        column: x => x.RequestTypeId,
                        principalTable: "RequestTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SPServiceATs_SPServices_SPServiceId",
                        column: x => x.SPServiceId,
                        principalTable: "SPServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SPServiceATs_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SPServiceATs_ServiceProviders_SPId",
                        column: x => x.SPId,
                        principalTable: "ServiceProviders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SPServiceATs_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SPServiceATs_Users_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SPServiceATs_Users_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "RequestTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Add" },
                    { 2, "Update" },
                    { 3, "Remove" },
                    { 4, "New" }
                });

            migrationBuilder.InsertData(
                table: "Sections",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Service" },
                    { 2, "Information" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "SuperAdmin" },
                    { 2, "HospitalAdmin" },
                    { 3, "DepartmentAdmin" },
                    { 4, "NormalUser" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_CreatedById",
                table: "Hospitals",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_UpdatedById",
                table: "Hospitals",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceATs_ApprovedById",
                table: "ServiceATs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceATs_RequestedById",
                table: "ServiceATs",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceATs_RequestTypeId",
                table: "ServiceATs",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceATs_ServiceId",
                table: "ServiceATs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviderATs_ApprovedById",
                table: "ServiceProviderATs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviderATs_RequestedById",
                table: "ServiceProviderATs",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviderATs_RequestTypeId",
                table: "ServiceProviderATs",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviderATs_SPId",
                table: "ServiceProviderATs",
                column: "SPId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_CreatedById",
                table: "ServiceProviders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceProviders_UpdatedById",
                table: "ServiceProviders",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CreatedById",
                table: "Services",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UpdatedById",
                table: "Services",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceATs_ApprovedById",
                table: "SPServiceATs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceATs_RequestedById",
                table: "SPServiceATs",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceATs_RequestTypeId",
                table: "SPServiceATs",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceATs_SectionId",
                table: "SPServiceATs",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceATs_ServiceId",
                table: "SPServiceATs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceATs_SPId",
                table: "SPServiceATs",
                column: "SPId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceATs_SPServiceId",
                table: "SPServiceATs",
                column: "SPServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferralATs_ApprovedById",
                table: "SPServiceReferralATs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferralATs_RequestedById",
                table: "SPServiceReferralATs",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferralATs_RequestTypeId",
                table: "SPServiceReferralATs",
                column: "RequestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferralATs_ServiceId",
                table: "SPServiceReferralATs",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferralATs_SPId",
                table: "SPServiceReferralATs",
                column: "SPId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferralATs_SPServiceReferralId",
                table: "SPServiceReferralATs",
                column: "SPServiceReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferrals_ServiceId",
                table: "SPServiceReferrals",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferrals_SPId",
                table: "SPServiceReferrals",
                column: "SPId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServiceReferrals_UpdatedById",
                table: "SPServiceReferrals",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SPServices_ServiceId",
                table: "SPServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServices_ServiceProviderId",
                table: "SPServices",
                column: "ServiceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_SPServices_UpdatedById",
                table: "SPServices",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_HospitalId",
                table: "Users",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserCreatedById",
                table: "Users",
                column: "UserCreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserUpdatedById",
                table: "Users",
                column: "UserUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_Users_CreatedById",
                table: "Hospitals",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_Users_UpdatedById",
                table: "Hospitals",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_Users_CreatedById",
                table: "Hospitals");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_Users_UpdatedById",
                table: "Hospitals");

            migrationBuilder.DropTable(
                name: "ServiceATs");

            migrationBuilder.DropTable(
                name: "ServiceProviderATs");

            migrationBuilder.DropTable(
                name: "SPServiceATs");

            migrationBuilder.DropTable(
                name: "SPServiceReferralATs");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "SPServices");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "RequestTypes");

            migrationBuilder.DropTable(
                name: "SPServiceReferrals");

            migrationBuilder.DropTable(
                name: "ServiceProviders");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Hospitals");
        }
    }
}
