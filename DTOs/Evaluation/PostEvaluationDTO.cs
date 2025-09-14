using static TrainingCenterAPI.Enums.Enums;

namespace TrainingCenterAPI.DTOs.Evaluation
{
    public class PostEvaluationDTO
    {
        public string evaluationOwner { get; set; }

        public string Opnion { get; set; }
        public int Rating { get; set; } // 1 to 5 stars


        public evaluationOwnerType evaluationOwnerType { get; set; }
    }
}
