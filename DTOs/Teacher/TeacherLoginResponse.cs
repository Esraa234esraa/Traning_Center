namespace TrainingCenterAPI.DTOs.Teacher
{
    public class TeacherLoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public Guid Id { get; set; }
    }
}
