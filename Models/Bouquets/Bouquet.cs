using System.ComponentModel.DataAnnotations.Schema;
using TrainingCenterAPI.Models.BaseEntitys;
using TrainingCenterAPI.Models.Courses;

namespace TrainingCenterAPI.Models.Bouquets
{
    public class Bouquet : BaseEntity
    {

        public string? BouquetName { get; set; }
        public Guid CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }

        public Guid LevelId { get; set; }
        [ForeignKey("LevelId")]
        public Level Level { get; set; }

        public int StudentsPackageCount { get; set; }
        public decimal Money { get; set; }

        public ICollection<Classes> Classes { get; set; } = new List<Classes>();
    }
}
