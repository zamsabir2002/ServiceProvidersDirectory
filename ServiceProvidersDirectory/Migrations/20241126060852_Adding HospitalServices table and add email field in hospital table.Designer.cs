﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceProvidersDirectory.Data;

#nullable disable

namespace ServiceProvidersDirectory.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241126060852_Adding HospitalServices table and add email field in hospital table")]
    partial class AddingHospitalServicestableandaddemailfieldinhospitaltable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServiceProvidersDirectory.Models.Hospital", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.RequestType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RequestTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Add"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Update"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Remove"
                        },
                        new
                        {
                            Id = 4,
                            Name = "New"
                        });
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.SPService", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HowToRefer")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SPId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceProviderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId");

                    b.HasIndex("ServiceProviderId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("SPServices");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.SPServiceAT", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApprovedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ApprovedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HowToRefer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RequestedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SPId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SPName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("SPServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("RequestTypeId");

                    b.HasIndex("RequestedById");

                    b.HasIndex("SPId");

                    b.HasIndex("SPServiceId");

                    b.HasIndex("SectionId");

                    b.HasIndex("ServiceId");

                    b.ToTable("SPServiceATs");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.SPServiceReferral", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("SPId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SPId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("UpdatedById");

                    b.ToTable("SPServiceReferrals");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.SPServiceReferralAT", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApprovedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ApprovedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocumentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<int>("RequestTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RequestedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SPId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SPServiceReferralId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("RequestTypeId");

                    b.HasIndex("RequestedById");

                    b.HasIndex("SPId");

                    b.HasIndex("SPServiceReferralId");

                    b.HasIndex("ServiceId");

                    b.ToTable("SPServiceReferralATs");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sections");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Service"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Information"
                        });
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.ServiceAT", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApprovedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ApprovedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RequestTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RequestedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("RequestTypeId");

                    b.HasIndex("RequestedById");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceATs");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.ServiceProvider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("ServiceProviders");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.ServiceProviderAT", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ApprovedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ApprovedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RequestedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SPId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedById");

                    b.HasIndex("RequestTypeId");

                    b.HasIndex("RequestedById");

                    b.HasIndex("SPId");

                    b.ToTable("ServiceProviderATs");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("HospitalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UserCreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserUpdatedById")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserCreatedById");

                    b.HasIndex("UserUpdatedById");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SuperAdmin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "HospitalAdmin"
                        },
                        new
                        {
                            Id = 3,
                            Name = "DepartmentAdmin"
                        },
                        new
                        {
                            Id = 4,
                            Name = "NormalUser"
                        });
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.Hospital", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ServiceProvidersDirectory.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.SPService", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.ServiceProvider", "SP")
                        .WithMany()
                        .HasForeignKey("ServiceProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("SP");

                    b.Navigation("Service");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.SPServiceAT", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ServiceProvidersDirectory.Models.RequestType", "RequestType")
                        .WithMany()
                        .HasForeignKey("RequestTypeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.User", "RequestedBy")
                        .WithMany()
                        .HasForeignKey("RequestedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ServiceProvidersDirectory.Models.ServiceProvider", "SP")
                        .WithMany()
                        .HasForeignKey("SPId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.SPService", "SPService")
                        .WithMany()
                        .HasForeignKey("SPServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("RequestType");

                    b.Navigation("RequestedBy");

                    b.Navigation("SP");

                    b.Navigation("SPService");

                    b.Navigation("Section");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.SPServiceReferral", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.ServiceProvider", "SP")
                        .WithMany()
                        .HasForeignKey("SPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("SP");

                    b.Navigation("Service");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.SPServiceReferralAT", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ServiceProvidersDirectory.Models.RequestType", "RequestType")
                        .WithMany()
                        .HasForeignKey("RequestTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.User", "RequestedBy")
                        .WithMany()
                        .HasForeignKey("RequestedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ServiceProvidersDirectory.Models.ServiceProvider", "SP")
                        .WithMany()
                        .HasForeignKey("SPId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.SPServiceReferral", "SPServiceReferral")
                        .WithMany()
                        .HasForeignKey("SPServiceReferralId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("RequestType");

                    b.Navigation("RequestedBy");

                    b.Navigation("SP");

                    b.Navigation("SPServiceReferral");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.Service", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("ServiceProvidersDirectory.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.ServiceAT", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("ServiceProvidersDirectory.Models.RequestType", "RequestType")
                        .WithMany()
                        .HasForeignKey("RequestTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.User", "RequestedBy")
                        .WithMany()
                        .HasForeignKey("RequestedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("RequestType");

                    b.Navigation("RequestedBy");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.ServiceProvider", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("ServiceProvidersDirectory.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.ServiceProviderAT", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.User", "ApprovedBy")
                        .WithMany()
                        .HasForeignKey("ApprovedById");

                    b.HasOne("ServiceProvidersDirectory.Models.RequestType", "RequestType")
                        .WithMany()
                        .HasForeignKey("RequestTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.User", "RequestedBy")
                        .WithMany()
                        .HasForeignKey("RequestedById");

                    b.HasOne("ServiceProvidersDirectory.Models.ServiceProvider", "SP")
                        .WithMany()
                        .HasForeignKey("SPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedBy");

                    b.Navigation("RequestType");

                    b.Navigation("RequestedBy");

                    b.Navigation("SP");
                });

            modelBuilder.Entity("ServiceProvidersDirectory.Models.User", b =>
                {
                    b.HasOne("ServiceProvidersDirectory.Models.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId");

                    b.HasOne("ServiceProvidersDirectory.Models.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ServiceProvidersDirectory.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("UserCreatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ServiceProvidersDirectory.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UserUpdatedById")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("CreatedBy");

                    b.Navigation("Hospital");

                    b.Navigation("Role");

                    b.Navigation("UpdatedBy");
                });
#pragma warning restore 612, 618
        }
    }
}
