using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenterAPI.Models.BaseEntitys;
using TrainingCenterAPI.Models.Courses;

public class Level : BaseEntity
{


    [Required]
    public int LevelNumber { get; set; } // من 1 إلى 7

    public string? Name { get; set; }     // اختياري: "Beginner", "Intermediate" ...

    // ✅ علاقات
    public Guid CourseId { get; set; }
    [ForeignKey("CourseId")]
    public Course Course { get; set; }
    //public ICollection<Classes> Classes { get; set; }
}
