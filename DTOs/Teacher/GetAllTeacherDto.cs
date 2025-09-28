namespace TrainingCenterAPI.DTOs.Teacher
{
    public class GetAllTeacherDto
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }


        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public int AvailableClasses { get; set; }

        public string Gender { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public Guid CourseId { get; set; }
    }
}
