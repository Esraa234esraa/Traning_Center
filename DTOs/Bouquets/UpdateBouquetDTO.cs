namespace TrainingCenterAPI.DTOs.Bouquets
{
    public class UpdateBouquetDTO
    {
        public string? BouquetName { get; set; }
        public Guid CourseId { get; set; }
        public Guid LevelId { get; set; }

        public int StudentsPackageCount { get; set; }
        public decimal Money { get; set; }
    }
}
