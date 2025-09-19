namespace TrainingCenterAPI.DTOs.Classes
{
    public class UpdateClassDTO
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? ClassTime { get; set; }



        public Guid BouquetId { get; set; }
    }
}
