using TrainingCenterAPI.Models.BaseEntitys;

namespace TrainingCenterAPI.Models.Courses
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public bool IsActive { get; set; } = true;


        public virtual ICollection<TeacherDetails> Teachers { get; set; } = new HashSet<TeacherDetails>();
        public virtual ICollection<Level> Levels { get; set; } = new HashSet<Level>();
    }
}
