using TrainingCenterAPI.DTOs.Notes;
using TrainingCenterAPI.Services.NotesServices;

namespace TrainingCenterAPI.Controllers.Notes
{

    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INotesService _notesService;

        public NoteController(INotesService notesService)
        {
            _notesService = notesService;
        }
        [HttpPost("AddNotesAsync")]
        public async Task<IActionResult> AddNotesAsync([FromForm] AddNoteDto dto)
        {
            var result = await _notesService.AddNotesAsync(dto);


            return Ok(result);
        }

        [HttpGet("GetAllNotesAsync")]
        public async Task<IActionResult> GetAllNotesAsync()
        {
            var result = await _notesService.GetAllNotesAsync();


            return Ok(result);
        }
        [HttpGet("GetAllStudentsForNote")]
        public async Task<IActionResult> GetAllStudentsForNote()
        {
            var result = await _notesService.GetAllStudentsForNote();


            return Ok(result);
        }



        [HttpGet("GetNotesByIdAsync/{Id}")]
        public async Task<IActionResult> GetNotesByIdAsync(Guid Id)
        {
            var result = await _notesService.GetNotesByIdAsync(Id);


            return Ok(result);
        }





        [HttpPut("UpdateNotesAsync/{Id}")]
        public async Task<IActionResult> UpdateNotesAsync(Guid Id, [FromForm] PutNoteDto dto)
        {
            var result = await _notesService.UpdateNotesAsync(Id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotesAsync(Guid id)
        {
            var result = await _notesService.DeleteNotesAsync(id);

            return Ok(result);

        }


    }
}

