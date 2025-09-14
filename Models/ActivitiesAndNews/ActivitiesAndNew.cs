using TrainingCenterAPI.Models.BaseEntitys;

namespace TrainingCenterAPI.Models.ActivitiesAndNews
{
    public class ActivitiesAndNew : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }

        public string FilePath { get; set; }
    }
}
