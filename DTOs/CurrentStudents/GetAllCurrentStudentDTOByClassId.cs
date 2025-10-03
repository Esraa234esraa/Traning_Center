namespace TrainingCenterAPI.DTOs.CurrentStudents
{
    public class GetAllCurrentStudentDTOByClassId
    {
        public Guid? Id { get; set; }
        public required string StudentName { get; set; }

        public string Email { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string? City { get; set; }
        public required string PhoneNumber { get; set; }

        public bool IsPaid { get; set; }
    }
}
