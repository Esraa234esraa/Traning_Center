using TrainingCenterAPI.DTOs.Notes;

namespace TrainingCenterAPI.Services.NotesServices
{
    public interface INotesService
    {
        Task<ResponseModel<List<GetNotesDto>>> GetAllNotesAsync();



        Task<ResponseModel<GetNotesDto>> GetNotesByIdAsync(Guid id);
        Task<ResponseModel<Guid>> AddNotesAsync(AddNoteDto courseDto);
        Task<ResponseModel<Guid>> UpdateNotesAsync(Guid id, PutNoteDto courseDto);
        Task<ResponseModel<bool>> DeleteNotesAsync(Guid id);
    }
}
