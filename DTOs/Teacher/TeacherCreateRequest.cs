namespace TrainingCenterAPI.DTOs.Teacher
{
    public class TeacherCreateRequest
    {
        // بيانات المعلم
        public TeacherDto Teacher { get; set; } = new TeacherDto();

        // كلمة المرور
        public string Password { get; set; } = string.Empty;
    }
}
