using Microsoft.AspNetCore.Mvc;
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

    }
}
