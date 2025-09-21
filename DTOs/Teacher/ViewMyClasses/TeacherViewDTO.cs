namespace TrainingCenterAPI.DTOs.Teacher.ViewMyClasses
{
    public class TeacherViewDTO
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public List<TeacherClassDtoView> Classes { get; set; } = new();
    }
    public class TeacherClassDtoView
    {
        public Guid Id { get; set; }
        public int LevelNumber { get; set; }   // 👈 المستوى كرقم
        public string? LevelName { get; set; }
        public int? PackageSize { get; set; }
        public int CurrentStudentsCount { get; set; }
        public ClassStatus Status { get; set; }   // ✅ الحالة
        public TimeSpan? ClassTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        public List<CurrentStudentForTeacherDTO>? Students { get; set; }
    }
    public class CurrentStudentForTeacherDTO
    {
        public string FullName { get; set; } = string.Empty;


    }
}
