using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace phonebook_data.Models
{
    public partial class phonebookContext : DbContext
    {
        public phonebookContext()
        {
        }

        public phonebookContext(DbContextOptions<phonebookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=IGNB\\IVANINSTANCE;Database=phonebook;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Croatian_CI_AS");

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Identifier);

                entity.ToTable("Department");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Identifier);

                entity.ToTable("Employee");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.JobDetails).IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.MobileNumber).IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.IdentifierDepartmentNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdentifierDepartment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.IdentifierLocationNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdentifierLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Location");

                entity.HasOne(d => d.IdentifierRoleNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdentifierRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Role");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Identifier);

                entity.ToTable("Location");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.Identifier);

                entity.ToTable("Permission");

                entity.HasOne(d => d.IdentifierDepartmentNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.IdentifierDepartment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Department");

                entity.HasOne(d => d.IdentifierEmployeeNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.IdentifierEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Employee");

                entity.HasOne(d => d.IdentifierRoleNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.IdentifierRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Role");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Identifier);

                entity.ToTable("Role");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
