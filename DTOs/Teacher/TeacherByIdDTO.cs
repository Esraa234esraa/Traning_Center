namespace TrainingCenterAPI.DTOs.Teacher
{
    public class TeacherByIdDTO
    {
        public Guid Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;



        public string CourseName { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
    }
}
