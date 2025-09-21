namespace TrainingCenterAPI.DTOs.CurrentStudents
{
    public class GetAllCurrentStudentDTO
    {
        public Guid? Id { get; set; }
        public required string StudentName { get; set; }

        public string Email { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string? City { get; set; }
        public required string PhoneNumber { get; set; }

        public bool IsPaid { get; set; } = false;
        public string? BouquetName { get; set; }
        public string? CourseName { get; set; }
        public int BouquetNumber { get; set; }
        public List<ClassForStudentDTO> Classes { get; set; } = new();
    }
    public class ClassForStudentDTO
    {
        public Guid ClassId { get; set; }
        public string BouquetName { get; set; }
        public int BouquetNumber { get; set; }
        public string CourseName { get; set; }
        public int LevelNumber { get; set; }
        public string LevelName { get; set; }
    }
}
