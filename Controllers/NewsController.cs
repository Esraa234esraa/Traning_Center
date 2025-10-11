using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Services.NewsServices;

namespace TrainingCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _courseService;

        public NewsController(INewsService courseService)
        {
            _courseService = courseService;
        }
        [HttpPost("AddNewsAsync")]
        public async Task<IActionResult> AddNewsAsync([FromForm] AddCoursesDto dto)
        {
            var result = await _courseService.AddNewsAsync(dto);


            return Ok(result);
        }
        [HttpGet("GetAllNewsAsync")]
        public async Task<IActionResult> GetAllNewsAsync()
        {
            var result = await _courseService.GetAllNewsAsync();


            return Ok(result);
        }

        [HttpGet("GetOnlyVisibleNewsAsync")]
        public async Task<IActionResult> GetOnlyVisibleNewsAsync()
        {
            var result = await _courseService.GetOnlyVisibleNewsAsync();


            return Ok(result);
        }

        [HttpGet("GetNewsByIdAsync/{Id}")]
        public async Task<IActionResult> GetNewsByIdAsync(Guid Id)
        {
            var result = await _courseService.GetNewsByIdAsync(Id);


            return Ok(result);
        }

        [HttpPut("HideNewsAsync/{Id}")]
        public async Task<IActionResult> HideNewsAsync(Guid Id)
        {
            var result = await _courseService.HideNewsAsync(Id);


            return Ok(result);
        }

        [HttpPut("VisibleNewsAsync/{Id}")]
        public async Task<IActionResult> VisibleNewsAsync(Guid Id)
        {
            var result = await _courseService.VisibleNewsAsync(Id);


            return Ok(result);
        }

        [HttpPut("UpdateNewsAsync/{Id}")]
        public async Task<IActionResult> UpdateNewsAsync(Guid Id, [FromForm] PutCourseDto dto)
        {
            var result = await _courseService.UpdateNewsAsync(Id, dto);


            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsAsync(Guid id)
        {
            var result = await _courseService.DeleteNewsAsync(id);

            return Ok(result);

        }


    }
}

