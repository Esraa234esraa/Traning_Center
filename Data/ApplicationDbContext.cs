using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<WaitingList> WaitingList { get; set; }
        public DbSet<StudentDetails> studentDetails { get; set; }
        public DbSet<Level> levels { get; set; }
        public DbSet<NewStudent> newStudents { get; set; }
        public DbSet<Evaluation> evaluations { get; set; }








        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -------------------
            // Seed Levels
            modelBuilder.Entity<Level>().HasData(
                new Level { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), LevelNumber = 1, Name = "المستوى الأول" },
                new Level { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), LevelNumber = 2, Name = "المستوى الثاني" },
                new Level { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), LevelNumber = 3, Name = "المستوى الثالث" },
                new Level { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), LevelNumber = 4, Name = "المستوى الرابع" },
                new Level { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), LevelNumber = 5, Name = "المستوى الخامس" },
                new Level { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), LevelNumber = 6, Name = "المستوى السادس" },
                new Level { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), LevelNumber = 7, Name = "المستوى السابع" }
            );




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
            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.Level)
                .WithMany()
                .HasForeignKey(sc => sc.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقات ApplicationUser -> Level
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Level)
                .WithMany()
                .HasForeignKey(u => u.LevelId)
                .OnDelete(DeleteBehavior.Restrict);


            // -------------------
            // StudentClass -> Class
            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.Class)
                .WithMany(c => c.StudentClasses)
                .HasForeignKey(sc => sc.ClassId)
                .OnDelete(DeleteBehavior.Restrict);

            // StudentClass -> Student
            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.Student)
                .WithMany()
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // WaitingList -> Class
            //modelBuilder.Entity<WaitingList>()
            //    .HasOne(w => w.Class)
            //    .WithMany(c => c.WaitingList)
            //    .HasForeignKey(w => w.ClassId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // WaitingList -> Student
            modelBuilder.Entity<WaitingList>()
                .HasOne(w => w.Student)
                .WithMany()
                .HasForeignKey(w => w.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // -------------------
            // Soft Delete filters
            modelBuilder.Entity<Classes>().HasQueryFilter(c => c.DeletedAt == null);
            modelBuilder.Entity<StudentClass>().HasQueryFilter(sc => sc.DeletedAt == null);
            modelBuilder.Entity<WaitingList>().HasQueryFilter(w => w.DeletedAt == null);
        }



    }
}
