using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.DTOs.Levels
{
    public class GetAllLevelsDTO
    {

        public Guid Id { get; set; }

        [Required]
        public int LevelNumber { get; set; } // من 1 إلى 7

        public string? Name { get; set; }
        public string? LevelName { get; set; }



        public string CourseName { get; set; }

    }
}
