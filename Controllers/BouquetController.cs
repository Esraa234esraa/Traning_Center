using TrainingCenterAPI.DTOs.Bouquets;

namespace TrainingCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BouquetController : ControllerBase
    {

        private readonly IBouquetService _bouquetService;

        public BouquetController(IBouquetService bouquetService)
        {
            _bouquetService = bouquetService;

        }
        [HttpPost("AddBouquet")]
        public async Task<IActionResult> AddBouquet([FromForm] AddBouquetDTO dto)
        {
            var result = await _bouquetService.AddBouquet(dto);


            return Ok(result);
        }

        [HttpGet("GetAllBouquets")]
        public async Task<IActionResult> GetAllBouquets()
        {
            var result = await _bouquetService.GetAllBouquets();


            return Ok(result);
        }
        [HttpGet("GetAllBouquetsOfLevel/{LevelId}")]
        public async Task<IActionResult> GetAllBouquetsOfLevel(Guid LevelId)
        {
            var result = await _bouquetService.GetAllBouquetsOfLevel(LevelId);


            return Ok(result);
        }


        [HttpPut("UpdateBouquet/{Id}")]
        public async Task<IActionResult> UpdateBouquet(Guid Id, [FromForm] UpdateBouquetDTO dto)
        {
            var result = await _bouquetService.UpdateBouquet(Id, dto);


            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBouquet(Guid id)
        {
            var result = await _bouquetService.DeleteBouquet(id);

            return Ok(result);

        }

    }
}

