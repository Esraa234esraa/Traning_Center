using TrainingCenterAPI.DTOs.BaseDTO;
using static TrainingCenterAPI.Enums.Enums;

namespace TrainingCenterAPI.DTOs.Evaluation
{
    public class GetAllEvaluationDTO : BaseDTOQuery
    {
        public Guid Id { get; set; }

        public string evaluationOwner { get; set; }

        public int Rating { get; set; } // 1 to 5 stars
        public string Opnion { get; set; }

        public evaluationOwnerType evaluationOwnerType { get; set; }

    }
}
