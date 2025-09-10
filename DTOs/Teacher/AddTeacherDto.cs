namespace TrainingCenterAPI.DTOs.Teacher
{
    public class AddTeacherDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // يحددها الأدمن
        public string Gender { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
    }
}
