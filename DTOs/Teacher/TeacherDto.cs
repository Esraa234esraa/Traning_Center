namespace TrainingCenterAPI.DTOs.Teacher
{
    // 1️⃣ بيانات أساسية للمعلم
    public class TeacherDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }  // 👈 Soft Delete
        public List<ClassDto> Classes { get; set; } = new();
        public int AvailableClasses { get; set; }

        public string Gender { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
    }

    // 2️⃣ المعلم + حصصه
    public class TeacherWithClassesDto : TeacherDto
    {

        public new List<ClassDto> Classes { get; set; } = new List<ClassDto>();

        public int ActiveClasses => Classes.Count(c =>
            c.Status == ClassStatus.Active &&
            c.CurrentStudentsCount == 0);

        public int PendingClasses => Classes.Count(c =>
            c.Status == ClassStatus.Pending &&
            c.PackageSize.HasValue &&
            c.CurrentStudentsCount > 0 &&
            c.CurrentStudentsCount < c.PackageSize.Value);

        public int CompletedClasses => Classes.Count(c =>
            c.Status == ClassStatus.Completed ||
            (c.PackageSize.HasValue && c.CurrentStudentsCount >= c.PackageSize.Value));

        public int CancelledClasses => Classes.Count(c =>
            c.Status == ClassStatus.Cancelled);

        public int AvailableClasses => Classes.Count(c =>
            (c.Status == ClassStatus.Active && c.CurrentStudentsCount == 0) ||
            (c.Status == ClassStatus.Pending &&
             c.PackageSize.HasValue &&
             c.CurrentStudentsCount > 0 &&
             c.CurrentStudentsCount < c.PackageSize.Value));
    }

    // بيانات الحصة الأساسية
    public class ClassDto
    {
        public Guid Id { get; set; }
        public int LevelNumber { get; set; }   // 👈 المستوى كرقم
        public string LevelName { get; set; }
        public int? PackageSize { get; set; }
        public int CurrentStudentsCount { get; set; }
        public ClassStatus Status { get; set; }   // ✅ الحالة
        public TimeSpan? ClassTime { get; set; }
        public DateTime? DeletedAt { get; set; }  // 👈 Soft Delete

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }


    // 3️⃣ الحصة + الطلاب
    public class ClassWithStudentsDto : ClassDto
    {
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();
    }


    // بيانات الطالب البسيطة
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsPaid { get; set; } // ✅ حالة الدفع
        public int LevelNumber { get; set; }   // 👈 المستوى كرقم
        public string LevelName { get; set; }
    }

    // 4️⃣ كل الطلاب لكل حصص المعلم
    public class AllStudentsForTeacherDto
    {
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public List<StudentDto> Students { get; set; } = new List<StudentDto>();
    }
}

