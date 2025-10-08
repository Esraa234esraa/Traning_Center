namespace TrainingCenterAPI.DTOs.Notes
{
    public class PutNoteDto
    {
        public Guid StudentId { get; set; }
        public required string Description { get; set; }
    }
}
