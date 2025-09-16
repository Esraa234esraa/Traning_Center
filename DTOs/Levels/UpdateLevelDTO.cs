using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.DTOs.Levels
{
    public class UpdateLevelDTO
    {

        [Required]
        public int LevelNumber { get; set; } // من 1 إلى 7

        public string Name { get; set; }
        public Guid CourseId { get; set; }
    }
}
