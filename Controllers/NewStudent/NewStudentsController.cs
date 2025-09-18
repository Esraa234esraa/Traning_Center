using TrainingCenterAPI.DTOs.NewStudents;
using TrainingCenterAPI.Services.NewStudentsService;

namespace TrainingCenterAPI.Controllers.NewStudent
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewStudentsController : ControllerBase
    {
        private readonly INewStudentsService _studentsService;
        public NewStudentsController(INewStudentsService newStudentsService)
        {
            _studentsService = newStudentsService;
        }
        [HttpGet("GetAllNewStudent")]
        public async Task<IActionResult> GetAllNewStudent()
        {

            return Ok(await _studentsService.GetAllNewStudent());
        }

        [HttpGet("GetAllWaitingNewStudent")]
        public async Task<IActionResult> GetAllWaitingNewStudent()
        {

            return Ok(await _studentsService.GetAllWaitingNewStudent());
        }


        [HttpPost("AddNewStudent")]
        public async Task<IActionResult> AddNewStudent(PostNewStudentDTO dTO)
        {

            return Ok(await _studentsService.AddNewStudent(dTO));
        }


        [HttpPut("PutNewStudent/{Id}")]
        public async Task<IActionResult> PutNewStudent(PutNewStudentDTO dTO, Guid Id)
        {

            return Ok(await _studentsService.PutNewStudent(dTO, Id));
        }

        [HttpPut("PutWaitingStudent/{Id}")]
        public async Task<IActionResult> PutWaitingStudent(PutNewStudentDTO dTO, Guid Id)
        {

            return Ok(await _studentsService.PutWaitingStudent(dTO, Id));
        }


        [HttpPut("MoveNewStudentToWaitingStudent/{Id}")]
        public async Task<IActionResult> MoveNewStudentToWaitingStudent(Guid Id)
        {

            return Ok(await _studentsService.MoveNewStudentToWaitingStudent(Id));
        }



        [HttpDelete("{Id}")]
        public async Task<IActionResult> PutNewStudent(Guid Id)
        {

            return Ok(await _studentsService.DeleteNewStudent(Id));
        }


        [HttpDelete("DeleteWaitingStudent/{Id}")]
        public async Task<IActionResult> DeleteWaitingStudent(Guid Id)
        {

            return Ok(await _studentsService.DeleteWaitingStudent(Id));
        }
    }
}
