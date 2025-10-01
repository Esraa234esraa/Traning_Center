using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Services.ExternalCoursesServices;

namespace TrainingCenterAPI.Controllers.ExternalExternalCoursesController
{
    public class ExternalExternalCourseController : ControllerBase
    {

        private readonly IExternalCoursesServices _courseService;

        public ExternalExternalCourseController(IExternalCoursesServices courseService)
        {
            _courseService = courseService;
        }
        [HttpPost("AddExternalCourse")]
        public async Task<IActionResult> AddExternalCourse([FromForm] AddCoursesDto dto)
        {
            var result = await _courseService.AddExternalCourseAsync(dto);


            return Ok(result);
        }
        [HttpGet("GetAllExternalCourses")]
        public async Task<IActionResult> GetAllExternalCourses()
        {
            var result = await _courseService.GetAllExternalCoursesAsync();


            return Ok(result);
        }


        [HttpGet("GetExternalCourseById/{Id}")]
        public async Task<IActionResult> GetExternalCourseById(Guid Id)
        {
            var result = await _courseService.GetExternalCourseByIdAsync(Id);


            return Ok(result);
        }

        [HttpPut("UpdateExternalCourse/{Id}")]
        public async Task<IActionResult> UpdateExternalCourseAsync(Guid Id, [FromForm] PutCourseDto dto)
        {
            var result = await _courseService.UpdateExternalCourseAsync(Id, dto);


            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExternalCourse(Guid id)
        {
            var result = await _courseService.DeleteExternalCourseAsync(id);

            return Ok(result);

        }


    }

}

