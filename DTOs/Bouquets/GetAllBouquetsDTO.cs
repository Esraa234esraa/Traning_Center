namespace TrainingCenterAPI.DTOs.Bouquets
{
    public class GetAllBouquetsDTO
    {
        public Guid Id { get; set; }
        public string? BouquetName { get; set; }
        public string CourseName { get; set; }
        public string? LevelName { get; set; }
        public int LevelNumber { get; set; }

        public int StudentsPackageCount { get; set; }
        public decimal Money { get; set; }
    }
}
