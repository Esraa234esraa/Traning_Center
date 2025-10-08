using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TrainingCenterAPI.Models.BaseEntitys;
using TrainingCenterAPI.Models.Bouquets;
using TrainingCenterAPI.Models.Courses;
using TrainingCenterAPI.Models.evaluations;
using TrainingCenterAPI.Models.ExternalCourses;
using TrainingCenterAPI.Models.Notes;
using TrainingCenterAPI.Models.Students;

namespace TrainingCenterAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }



        // جداول
        public DbSet<TeacherDetails> TeacherDetails { get; set; }
        public DbSet<OtpVerification> OtpVerifications { get; set; }
        public DbSet<EmailVerification> EmailVerifications { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Note> notes { get; set; }



        public DbSet<Level> levels { get; set; }
        public DbSet<NewStudent> newStudents { get; set; }
        public DbSet<Evaluation> evaluations { get; set; }
        public DbSet<Bouquet> bouquets { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<ExternalCourse> ExternalCourses { get; set; }
        public DbSet<CurrentStudent> currents { get; set; }
        public DbSet<CurrentStudentClass> CurrentStudentClasses { get; set; }










        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Classes>()
      .HasOne(c => c.Teacher)
      .WithMany(t => t.Classes)
      .HasForeignKey(c => c.TeacherId)
      .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Classes>().HasQueryFilter(c => c.DeletedAt == null);
            //   modelBuilder.Entity<StudentClass>().HasQueryFilter(sc => sc.DeletedAt == null);

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }



}

