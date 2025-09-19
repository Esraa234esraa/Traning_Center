namespace TrainingCenterAPI.DTOs.Classes
{
    public class AddClassDTO
    {

        //    public Guid? TeacherId { get; set; }


        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? ClassTime { get; set; }



        public Guid BouquetId { get; set; }


        //must remove this

    }
}
