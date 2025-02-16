using HochschuleApp.entity;
using Microsoft.EntityFrameworkCore;

namespace HochschuleApp.context
{
    public class HochschuleContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Semester> Semesters { get; set; }

        public HochschuleContext(DbContextOptions<HochschuleContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id); // Define Id as the primary key

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50); // Set a maximum length for Name

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255); // Set a maximum length for Description

                entity.Property(e => e.Startdate); // No explicit configuration needed

                entity.Property(e => e.Enddate); // No explicit configuration needed

                entity.HasMany(e => e.Semesters)
                .WithMany(e => e.Courses)
                .UsingEntity(
                    l => l.HasOne(typeof(Semester)).WithMany().HasForeignKey("SemesterForeignKey"),
                    r => r.HasOne(typeof(Course)).WithMany().HasForeignKey("CourseForeignKey"));

                entity.HasMany(e => e.Students)
                .WithMany(e => e.Courses)
                .UsingEntity(
                    l => l.HasOne(typeof(Student)).WithMany().HasForeignKey("StudentForeignKey"),
                    r => r.HasOne(typeof(Course)).WithMany().HasForeignKey("CourseForeignKey"));
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasOne(e => e.Semester)
                .WithMany(e => e.Students);
            });  
        }
    }
}
