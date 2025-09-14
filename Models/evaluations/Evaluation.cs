using TrainingCenterAPI.Models.BaseEntitys;
using static TrainingCenterAPI.Enums.Enums;

namespace TrainingCenterAPI.Models.evaluations
{
    public class Evaluation : BaseEntity
    {
        public string evaluationOwner { get; set; }

        public int Rating { get; set; } // 1 to 5 stars
        public string Opnion { get; set; }

        public evaluationOwnerType evaluationOwnerType { get; set; }

    }
}
