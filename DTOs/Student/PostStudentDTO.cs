namespace TrainingCenterAPI.DTOs.Student
{
    public class PostStudentDTO
    {


        public string FullName { get; set; }
        public string Email { get; set; } = string.Empty;
        public int PackageSize { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsPaid { get; set; } // ✅ حالة الدفع
        public Guid LevelNumber { get; set; }
        public string? CourseName { get; set; }
        public Guid ClassId { get; set; }
        public DateTime StartDate { get; set; }
        public string City { get; set; } = string.Empty;

        public Guid TeacherId { get; set; }

        public decimal Money { get; set; }

    }
}
