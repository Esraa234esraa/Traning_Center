namespace TrainingCenterAPI.Controllers.Level
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {

        private readonly ILevelService _levelService;

        public LevelController(ILevelService levelService)
        {
            _levelService = levelService;
        }
        [HttpPost("AddLevel")]
        public async Task<IActionResult> AddLevel([FromForm] AddLevelDTO dto)
        {
            var result = await _levelService.AddLevel(dto);


            return Ok(result);
        }
        [HttpGet("GetAllLevelsOfCourse/{CourseId}")]
        public async Task<IActionResult> GetAllLevelsOfCourse(Guid CourseId)
        {
            var result = await _levelService.GetAllLevelsOfCourse(CourseId);


            return Ok(result);
        }
        [HttpGet("GetAllLevels")]
        public async Task<IActionResult> GetAllLevels()
        {
            var result = await _levelService.GetAllLevels();


            return Ok(result);
        }
        [HttpGet("GetLevelById/{Id}")]
        public async Task<IActionResult> GetLevelById(Guid Id)
        {
            var result = await _levelService.GetLevelByIdAsync(Id);


            return Ok(result);
        }

        [HttpPut("UpdateLevel/{Id}")]
        public async Task<IActionResult> UpdateLevel(Guid Id, [FromForm] UpdateLevelDTO dto)
        {
            var result = await _levelService.UpdateLevel(Id, dto);


            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLevel(Guid id)
        {
            var result = await _levelService.DeleteLevel(id);

            return Ok(result);

        }

    }
}
