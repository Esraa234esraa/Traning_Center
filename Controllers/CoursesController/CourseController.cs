using TrainingCenterAPI.DTOs.Courses;

namespace TrainingCenterAPI.Controllers.CoursesController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly ICoursesServices _courseService;

        public CourseController(ICoursesServices courseService)
        {
            _courseService = courseService;
        }
        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse([FromForm] AddCoursesDto dto)
        {
            var result = await _courseService.AddCourseAsync(dto);


            return Ok(result);
        }
        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var result = await _courseService.GetAllCoursesAsync();


            return Ok(result);
        }


        [HttpGet("GetCourseById/{Id}")]
        public async Task<IActionResult> GetCourseById(Guid Id)
        {
            var result = await _courseService.GetCourseByIdAsync(Id);


            return Ok(result);
        }

        [HttpPut("UpdateCourse/{Id}")]
        public async Task<IActionResult> UpdateCourseAsync(Guid Id, [FromForm] PutCourseDto dto)
        {
            var result = await _courseService.UpdateCourseAsync(Id, dto);


            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var result = await _courseService.DeleteCourseAsync(id);

            return Ok(result);

        }




    }
}
