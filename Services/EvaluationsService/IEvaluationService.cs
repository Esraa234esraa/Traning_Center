using TrainingCenterAPI.DTOs.Evaluation;

namespace TrainingCenterAPI.Services.EvaluationsService
{
    public interface IEvaluationService
    {
        Task<ResponseModel<Guid>> AddEvaluation(PostEvaluationDTO DTO);

        Task<ResponseModel<List<GetAllEvaluationDTO>>> GetAllEvaluation();


        Task<ResponseModel<string>> DeleteEvaluation(Guid Id);
    }
}
