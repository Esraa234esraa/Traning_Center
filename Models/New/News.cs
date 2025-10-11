using TrainingCenterAPI.Models.BaseEntitys;

namespace TrainingCenterAPI.Models.New
{
    public class News : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;

    }
}
