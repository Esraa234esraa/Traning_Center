namespace TrainingCenterAPI.DTOs.Bouquets
{
    public class AddBouquetDTO
    {
        public string? BouquetName { get; set; }
        public Guid CourseId { get; set; }
        public Guid LevelId { get; set; }

        public int StudentsPackageCount { get; set; }
        public decimal Money { get; set; }

    }
}
