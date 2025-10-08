namespace TrainingCenterAPI.DTOs.Notes
{
    public class GetNotesDto
    {
        public Guid Id { get; set; }
        public required string Description { get; set; }
        public required string StudentName { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
