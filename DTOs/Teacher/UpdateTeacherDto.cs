namespace TrainingCenterAPI.DTOs.Teacher
{
    public class UpdateTeacherDto
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;


        public string City { get; set; } = string.Empty;

        public Guid CourseId { get; set; }
    }
}
