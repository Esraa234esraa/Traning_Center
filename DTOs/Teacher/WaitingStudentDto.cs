namespace TrainingCenterAPI.DTOs.Teacher
{
    public class WaitingStudentDto
    {
        public Guid StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? DeletedAt { get; set; }  

        public DateTime AddedAt { get; set; }
    }
}
