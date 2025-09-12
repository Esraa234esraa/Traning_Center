namespace TrainingCenterAPI.DTOs.Student
{
    public class GetCurrentStudentDTO
    {
        public string FullName { get; set; }
        public int? PackageSize { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public bool IsPaid { get; set; } // ✅ حالة الدفع
        public string? CourseName { get; set; }
        public Guid ClassId { get; set; }
        public string City { get; set; } = string.Empty;

        public string TeacherName { get; set; }

    }
}
