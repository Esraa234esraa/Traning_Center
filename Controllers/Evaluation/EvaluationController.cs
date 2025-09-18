using TrainingCenterAPI.DTOs.Evaluation;
using TrainingCenterAPI.Services.EvaluationsService;

namespace TrainingCenterAPI.Controllers.Evaluation
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _evaluationService;

        public EvaluationController(IEvaluationService evaluationService)
        {

            _evaluationService = evaluationService;
        }
        [HttpGet("GetAllEvaluation")]
        public async Task<IActionResult> GetAllEvaluation()

        {
            return Ok(await _evaluationService.GetAllEvaluation());
        }
        [HttpGet("GetOnlyVisibleEvaluations")]
        public async Task<IActionResult> GetOnlyVisibleEvaluations()
        {
            var result = await _evaluationService.GetOnlyVisibleEvaluationsAsync();


            return Ok(result);
        }
        [HttpPost("AddEvaluation")]
        public async Task<IActionResult> AddEvaluation(PostEvaluationDTO dTO)

        {
            return Ok(await _evaluationService.AddEvaluation(dTO));
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEvaluation(Guid Id)

        {
            return Ok(await _evaluationService.DeleteEvaluation(Id));
        }
        [HttpPut("HideEvaluation/{id}")]
        public async Task<IActionResult> HideEvaluation(Guid id)
        {
            var result = await _evaluationService.HideEvaluationAsync(id);

            return Ok(result);

        }
        [HttpPut("VisibleEvaluation/{id}")]

        public async Task<IActionResult> VisibleEvaluation(Guid id)
        {
            var result = await _evaluationService.VisibleEvaluationAsync(id);

            return Ok(result);

        }

    }
}
