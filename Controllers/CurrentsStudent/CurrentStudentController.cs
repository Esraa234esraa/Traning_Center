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
        [HttpPost("AddLevel")]
        public async Task<IActionResult> AddLevel([FromForm] AddCurrentStudentDTO dto)
        {
            var result = await _currentStudentService.AddCurrentStudent(dto);


            return Ok(result);
        }
    }
}
