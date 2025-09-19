using TrainingCenterAPI.DTOs.Evaluation;

namespace TrainingCenterAPI.Services.EvaluationsService
{
    public interface IEvaluationService
    {
        Task<ResponseModel<Guid>> AddEvaluation(PostEvaluationDTO DTO);

        Task<ResponseModel<ResponseDTO>> GetAllEvaluation(GetAllEvaluationQuery request);

        Task<ResponseModel<bool>> HideEvaluationAsync(Guid id);
        Task<ResponseModel<bool>> VisibleEvaluationAsync(Guid id);
        Task<ResponseModel<List<GetAllEvaluationDTO>>> GetOnlyVisibleEvaluationsAsync();
        Task<ResponseModel<string>> DeleteEvaluation(Guid Id);
    }
}
