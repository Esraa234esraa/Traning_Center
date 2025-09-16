using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TrainingCenterAPI.Models.Bouquets;
using TrainingCenterAPI.Models.Courses;
using TrainingCenterAPI.Models.evaluations;
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
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Classes> Classes { get; set; }



        public DbSet<Level> levels { get; set; }
        public DbSet<NewStudent> newStudents { get; set; }
        public DbSet<Evaluation> evaluations { get; set; }
        public DbSet<Bouquet> bouquets { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CurrentStudent> currents { get; set; }
        public DbSet<CurrentStudentClass> CurrentStudentClasses { get; set; }










        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------




            modelBuilder.Entity<NewStudent>()
                .HasIndex(s => new { s.Date, s.Time })
                .IsUnique(); // ensures uniqueness at DB level



            // -------------------
            // علاقات Classes -> Level
            modelBuilder.Entity<Classes>()
                    .HasOne(c => c.Level)
                    .WithMany()
                    .HasForeignKey(c => c.LevelId)
                    .OnDelete(DeleteBehavior.Restrict);

            // علاقات StudentClass -> Level
            //modelBuilder.Entity<StudentClass>()
            //    .HasOne(sc => sc.Level)
            //    .WithMany()
            //    .HasForeignKey(sc => sc.LevelId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // علاقات ApplicationUser -> Level
            //modelBuilder.Entity<ApplicationUser>()
            //    .HasOne(u => u.Level)
            //    .WithMany()
            //    .HasForeignKey(u => u.LevelId)
            //    .OnDelete(DeleteBehavior.Restrict);


            // -------------------
            // StudentClass -> Class
            //modelBuilder.Entity<StudentClass>()
            //    .HasOne(sc => sc.Class)
            //    .WithMany(c => c.StudentClasses)
            //    .HasForeignKey(sc => sc.ClassId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // StudentClass -> Student
            //modelBuilder.Entity<StudentClass>()
            //    .HasOne(sc => sc.Student)
            //    .WithMany()
            //    .HasForeignKey(sc => sc.StudentId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // WaitingList -> Class
            //modelBuilder.Entity<WaitingList>()
            //    .HasOne(w => w.Class)
            //    .WithMany(c => c.WaitingList)
            //    .HasForeignKey(w => w.ClassId)
            //    .OnDelete(DeleteBehavior.Restrict);




            // -------------------
            // Soft Delete filters
            modelBuilder.Entity<Classes>().HasQueryFilter(c => c.DeletedAt == null);
            //   modelBuilder.Entity<StudentClass>().HasQueryFilter(sc => sc.DeletedAt == null);

        }



    }
}
