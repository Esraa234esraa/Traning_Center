using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.DTOs.Classes
{
    public class GetAllClassesOfBouquetDTO
    {
        public Guid Id { get; set; }

        public Guid? TeacherId { get; set; }
        public int BouquetCount { get; set; }
        public int CurrentStudentsCount { get; set; }

        [Required]
        public ClassStatus Status { get; set; } // ✅ حالة واحدة

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? ClassTime { get; set; }

        public string? BouquetName { get; set; }
        public Guid BouquetId { get; set; }

    }
}
