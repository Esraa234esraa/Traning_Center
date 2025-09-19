namespace TrainingCenterAPI.DTOs.CurrentStudents
{
    public class AddCurrentStudentDTO
    {
        public required string StudentName { get; set; }


        public string? Gender { get; set; }
        public string? City { get; set; }
        public required string PhoneNumber { get; set; }

        public bool IsPaid { get; set; } = false;
        public Guid ClassId { get; set; }
    }
}
