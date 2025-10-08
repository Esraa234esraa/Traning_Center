namespace TrainingCenterAPI.DTOs.Notes
{
    public class AddNoteDto
    {
        public Guid StudentId { get; set; }
        public required string Description { get; set; }

    }
}
