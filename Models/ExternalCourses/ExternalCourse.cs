using TrainingCenterAPI.Models.BaseEntitys;

namespace TrainingCenterAPI.Models.ExternalCourses
{
    public class ExternalCourse : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public bool IsActive { get; set; } = true;


    }
}
