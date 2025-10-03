using TrainingCenterAPI.DTOs.CurrentStudents;
using TrainingCenterAPI.Services.CurretnStudentsService;

namespace TrainingCenterAPI.Controllers.CurrentsStudent
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentStudentController : ControllerBase
    {



        private readonly ICurrentStudentService _currentStudentService;

        public CurrentStudentController(ICurrentStudentService currentStudentService)
        {
            _currentStudentService = currentStudentService;
        }
        [HttpPost("AddCurrentStudent")]
        public async Task<IActionResult> AddCurrentStudent([FromForm] AddCurrentStudentDTO dto)
        {
            var result = await _currentStudentService.AddCurrentStudent(dto);


            return Ok(result);
        }
        [HttpGet("GetAllCurrentStudent")]
        public async Task<IActionResult> GetAllCurrentStudent([FromQuery] GetAllCurrentStudentQuery request)
        {
            var result = await _currentStudentService.GetAllCurrentStudent(request);


            return Ok(result);
        }
        [HttpGet("GetAllCurrentStudentByClassId/{classId}")]
        public async Task<IActionResult> GetAllCurrentStudentByClassId(Guid classId)
        {
            var result = await _currentStudentService.GetAllCurrentStudentByClassId(classId);


            return Ok(result);
        }
        [HttpPut("UpdateCurrentStudent/{Id}")]
        public async Task<IActionResult> UpdateCurrentStudent(Guid Id, [FromForm] UpdateCurrentStudentDTO dto)
        {
            var result = await _currentStudentService.UpdateCurrentStudent(Id, dto);


            return Ok(result);
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCurrentStudent(Guid Id)
        {
            var result = await _currentStudentService.DeleteCurrentStudent(Id);


            return Ok(result);
        }
    }
}
