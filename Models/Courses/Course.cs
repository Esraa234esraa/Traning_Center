using TrainingCenterAPI.Models.BaseEntitys;

namespace TrainingCenterAPI.Models.Courses
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public virtual ICollection<Classes> Classes { get; set; } = new HashSet<Classes>();
        public virtual ICollection<ApplicationUser> Teachers { get; set; } = new HashSet<ApplicationUser>();
    }
}
