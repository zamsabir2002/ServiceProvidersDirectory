using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using ServiceProvidersDirectory.Models;

namespace ServiceProvidersDirectory.Data
{

    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Hospital> Hospitals => Set<Hospital>();
        public DbSet<RequestType> RequestTypes => Set<RequestType>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<ServiceAT> ServiceATs => Set<ServiceAT>();
        public DbSet<Models.ServiceProvider> ServiceProviders => Set<Models.ServiceProvider>();
        public DbSet<ServiceProviderAT> ServiceProviderATs => Set<ServiceProviderAT>();
        public DbSet<SPService> SPServices => Set<SPService>();
        public DbSet<SPServiceAT> SPServiceATs => Set<SPServiceAT>();
        public DbSet<SPServiceReferral> SPServiceReferrals => Set<SPServiceReferral>();
        public DbSet<SPServiceReferralAT> SPServiceReferralATs => Set<SPServiceReferralAT>();

        public DbSet<HospitalService> HospitalServices => Set<HospitalService>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Configure relationship for Hospital.CreatedBy (many-to-one with User)
            modelBuilder.Entity<Hospital>()
                .HasOne(h => h.CreatedBy) // Hospital has one CreatedBy
                .WithMany() // User has many Hospitals created
                .HasForeignKey(h => h.CreatedById) // ForeignKey is CreatedById
                .OnDelete(DeleteBehavior.Cascade); // Optional: Decide what happens on delete (SetNull, Cascade, etc.)

            modelBuilder.Entity<Hospital>()
                .HasOne(h => h.UpdatedBy) // Hospital has one UpdatedBy
                .WithMany() // User has many Hospitals updated
                .HasForeignKey(h => h.UpdatedById) // ForeignKey is UpdatedById
                .OnDelete(DeleteBehavior.Cascade); // Optional: Delete behavior

            // Configure relationship for User.CreatedBy and User.UpdatedBy (self-referencing)
            modelBuilder.Entity<User>()
                .HasOne(u => u.CreatedBy) // User has one CreatedBy
                .WithMany() // A user can create many users
                .HasForeignKey(u => u.UserCreatedById) // ForeignKey is CreatedById
                .OnDelete(DeleteBehavior.NoAction); // Optional: Delete behavior

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) // User has one CreatedBy
                .WithMany() // A user can create many users
                .HasForeignKey(u => u.RoleId) // ForeignKey is CreatedById
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UpdatedBy) // User has one UpdatedBy
                .WithMany() // A user can update many users
                .HasForeignKey(u => u.UserUpdatedById) // ForeignKey is UpdatedById
                .OnDelete(DeleteBehavior.NoAction); // Optional: Delete behavior

            modelBuilder.Entity<SPServiceReferralAT>()
               .HasOne(r => r.RequestType)
               .WithMany()
               .HasForeignKey(r => r.RequestTypeId)
               .OnDelete(DeleteBehavior.Restrict);  // Set delete behavior to Restrict for this FK

            modelBuilder.Entity<SPServiceReferralAT>()
                .HasOne(r => r.SPServiceReferral)
                .WithMany()
                .HasForeignKey(r => r.SPServiceReferralId)
                .OnDelete(DeleteBehavior.Restrict);  // Restrict cascade delete on SPServiceReferralId

            modelBuilder.Entity<SPServiceReferralAT>()
                .HasOne(r => r.SP)
                .WithMany()
                .HasForeignKey(r => r.SPId)
                .OnDelete(DeleteBehavior.Restrict);  // Restrict delete for ServiceProvider

            modelBuilder.Entity<SPServiceReferralAT>()
                .HasOne(r => r.Service)
                .WithMany()
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);  // Restrict delete for Service

            modelBuilder.Entity<SPServiceReferralAT>()
                .HasOne(r => r.RequestedBy)
                .WithMany()
                .HasForeignKey(r => r.RequestedById)
                .OnDelete(DeleteBehavior.NoAction);  // Set null for RequestedBy on delete

            modelBuilder.Entity<SPServiceReferralAT>()
                .HasOne(r => r.ApprovedBy)
                .WithMany()
                .HasForeignKey(r => r.ApprovedById)
                .OnDelete(DeleteBehavior.NoAction);  // Set null for ApprovedBy on delete


            modelBuilder.Entity<SPServiceAT>()
                .HasOne(r => r.SPService)
                .WithMany()
                .HasForeignKey(r => r.SPServiceId)
                .OnDelete(DeleteBehavior.Cascade); // Assuming cascading delete for SPService

            modelBuilder.Entity<SPServiceAT>()
                .HasOne(r => r.SP)
                .WithMany()
                .HasForeignKey(r => r.SPId)
                .OnDelete(DeleteBehavior.NoAction); // Assuming cascading delete for ServiceProvider

            modelBuilder.Entity<SPServiceAT>()
                .HasOne(r => r.Service)
                .WithMany()
                .HasForeignKey(r => r.ServiceId)
                .OnDelete(DeleteBehavior.NoAction); // Assuming cascading delete for Service

            modelBuilder.Entity<SPServiceAT>()
                .HasOne(r => r.RequestType)
                .WithMany()
                .HasForeignKey(r => r.RequestTypeId)
                .OnDelete(DeleteBehavior.NoAction); // Assuming cascading delete for RequestType

            modelBuilder.Entity<SPServiceAT>()
                .HasOne(r => r.Section)
                .WithMany()
                .HasForeignKey(r => r.SectionId)
                .OnDelete(DeleteBehavior.NoAction); // Assuming cascading delete for Section

            // Set NO ACTION for RequestedBy relationship (no cascading delete)
            modelBuilder.Entity<SPServiceAT>()
                .HasOne(r => r.RequestedBy)
                .WithMany()
                .HasForeignKey(r => r.RequestedById)
                .OnDelete(DeleteBehavior.NoAction);  // Set no action for RequestedBy

            // Set NO ACTION for ApprovedBy relationship (no cascading delete)
            modelBuilder.Entity<SPServiceAT>()
                .HasOne(r => r.ApprovedBy)
                .WithMany()
                .HasForeignKey(r => r.ApprovedById)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Hospital>()
                .HasOne(r => r.CreatedBy)
                .WithMany()
                .HasForeignKey(r => r.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);  // Set no action for RequestedBy

            // Set NO ACTION for ApprovedBy relationship (no cascading delete)
            modelBuilder.Entity<Hospital>()
                .HasOne(r => r.UpdatedBy)
                .WithMany()
                .HasForeignKey(r => r.UpdatedById)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<UserRole>().HasData(
                    new UserRole { Id = 1, Name = "SuperAdmin" },
                    new UserRole { Id = 2, Name = "HospitalAdmin" },
                    new UserRole { Id = 3, Name = "DepartmentAdmin" },
                    new UserRole { Id = 4, Name = "NormalUser" }
                );
            modelBuilder.Entity<RequestType>().HasData(
                new RequestType { Id = 1, Name = "Add" },
                new RequestType { Id = 2, Name = "Update" },
                new RequestType { Id = 3, Name = "Remove" },
                new RequestType { Id = 4, Name = "New" }
            );
            modelBuilder.Entity<Section>().HasData(
                new Section { Id = 1, Name = "Service" },
                new Section { Id = 2, Name = "Information" }
            );
        }
    }

}