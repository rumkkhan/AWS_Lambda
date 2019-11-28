using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace linqq.Models
{
    public partial class PlutoCodeFirstContext : DbContext
    {
        public PlutoCodeFirstContext()
        {
        }

        public PlutoCodeFirstContext(DbContextOptions<PlutoCodeFirstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<TagCourses> TagCourses { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PlutoCodeFirst;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Courses>(entity =>
            {
                entity.Property(e => e.AuthorId).HasColumnName("Author_Id");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_dbo.Courses_dbo.Authors_Author_Id");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<TagCourses>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.CourseId })
                    .HasName("PK_dbo.TagCourses");

                entity.Property(e => e.TagId).HasColumnName("Tag_Id");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TagCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_dbo.TagCourses_dbo.Courses_Course_Id");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagCourses)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_dbo.TagCourses_dbo.Tags_Tag_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
