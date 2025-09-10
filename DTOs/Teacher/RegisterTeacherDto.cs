using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.DTOs.Teacher
{
    public class RegisterTeacherDto
    {
        public string FullName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        // بيانات المعلم الإضافية
        public string Gender { get; set; }
        public string City { get; set; }
        public string CourseName { get; set; }
    }
}
