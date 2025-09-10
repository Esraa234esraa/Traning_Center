using System.ComponentModel.DataAnnotations;
using TrainingCenterAPI.Models;

public class Level
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public int LevelNumber { get; set; } // من 1 إلى 7

    public string Name { get; set; }     // اختياري: "Beginner", "Intermediate" ...

    // ✅ علاقات
    public ICollection<ApplicationUser> Students { get; set; }
    public ICollection<Classes> Classes { get; set; }
}
