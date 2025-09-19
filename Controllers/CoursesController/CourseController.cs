using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Services.CoursesServices;

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
        [HttpGet("GetOnlyVisibleCourses")]
        public async Task<IActionResult> GetOnlyVisibleCourses()
        {
            var result = await _courseService.GetOnlyVisibleCoursesAsync();


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
        [HttpPut("HideCourse/{id}")]

        public async Task<IActionResult> HideCourse(Guid id)
        {
            var result = await _courseService.HideCourseAsync(id);

            return Ok(result);

        }
        [HttpPut("VisibleCourse/{id}")]

        public async Task<IActionResult> VisibleCourse(Guid id)
        {
            var result = await _courseService.VisibleCourseAsync(id);

            return Ok(result);

        }

    }
}
