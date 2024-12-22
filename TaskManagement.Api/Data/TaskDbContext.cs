using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Models;

namespace TaskManagement.Api.Data
{
    public class TaskDbContext : IdentityDbContext<IdentityUser>
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet für TaskEntities.
        /// </summary>
        public DbSet<TaskEntity> Tasks { get; set; }

        /// <summary>
        /// Konfiguration der Modelle und Beziehungen.
        /// </summary>
        /// <param name="modelBuilder">Der ModelBuilder für EF Core.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identity Tables Customization (optional)
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable("Users");
            });

            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable("Roles");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            // TaskEntity-Konfiguration
            modelBuilder.Entity<TaskEntity>(entity =>
            {
                entity.ToTable("Tasks");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(t => t.Description)
                    .HasMaxLength(1000);

                entity.Property(t => t.Status)
                    .IsRequired();

                entity.Property(t => t.CreatedAt)
                    .IsRequired();

                entity.Property(t => t.DueDate);
            });
        }
    }
}
