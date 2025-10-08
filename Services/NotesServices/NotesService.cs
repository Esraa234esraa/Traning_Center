using TrainingCenterAPI.DTOs.Notes;
using TrainingCenterAPI.Models.Notes;

namespace TrainingCenterAPI.Services.NotesServices
{
    public class NotesService : INotesService
    {
        private readonly ApplicationDbContext _context;
        public NotesService(ApplicationDbContext context)
        {

            _context = context;
        }
        public async Task<ResponseModel<Guid>> AddNotesAsync(AddNoteDto Dto)
        {
            try
            {



                var note = new Note
                {

                    CurrentStudentId = Dto.StudentId,
                    Description = Dto.Description,


                };
                _context.notes.Add(note);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(note.Id, "تمت الاضافة بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت الاضافة  ");
            }
        }

        public async Task<ResponseModel<bool>> DeleteNotesAsync(Guid id)
        {
            try
            {
                var note = await _context.notes.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
                if (note == null)
                    return ResponseModel<bool>.FailResponse("الملاحظة ليست موجودة");

                // 👇 Soft delete
                note.DeletedAt = DateTime.UtcNow;

                note.IsDeleted = true;
                _context.notes.Update(note);
                await _context.SaveChangesAsync();
                return ResponseModel<bool>.SuccessResponse(true, "نم نقل الملاحظة الى سلة المهملات");
            }
            catch (Exception ex)
            {

                return ResponseModel<bool>.FailResponse($"{ex.Message}  فشلت عملية الحذف ");

            }
        }

        public async Task<ResponseModel<List<GetNotesDto>>> GetAllNotesAsync()
        {
            var notes = await _context.notes.Where(x => x.IsDeleted == false)
                   .OrderByDescending(x => x.CreatedAt)
                   .AsNoTracking()

                   .Select(c => new GetNotesDto
                   {
                       Id = c.Id,
                       StudentName = c.CurrentStudent.StudentName,
                       Description = c.Description,
                       CreateAt = c.CreatedAt

                   })
                   .ToListAsync();
            if (notes.Count() <= 0)
                return ResponseModel<List<GetNotesDto>>.FailResponse("  لا توجد ملاحظات");

            return ResponseModel<List<GetNotesDto>>.SuccessResponse(notes, "notes retrieved successfully");
        }

        public async Task<ResponseModel<GetNotesDto>> GetNotesByIdAsync(Guid id)
        {
            var Note = await _context.notes.Include(x => x.CurrentStudent).FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false);
            if (Note == null)
                return ResponseModel<GetNotesDto>.FailResponse("الملاحظة ليست موجودة");
            var note = new GetNotesDto
            {
                Id = Note.Id,
                Description = Note.Description,
                StudentName = Note.CurrentStudent.StudentName,
                CreateAt = Note.CreatedAt
            };

            return ResponseModel<GetNotesDto>.SuccessResponse(note, "notes retrieved successfully");
        }

        public async Task<ResponseModel<Guid>> UpdateNotesAsync(Guid id, PutNoteDto Dto)
        {
            try
            {
                var oldNote = await _context.notes.FirstOrDefaultAsync(c => c.Id == id);
                if (oldNote == null)
                {
                    return ResponseModel<Guid>.FailResponse($"هذا الكورس غير موجود ");
                }



                oldNote.CurrentStudentId = Dto.StudentId;
                oldNote.Description = Dto.Description;

                _context.notes.Update(oldNote);
                await _context.SaveChangesAsync();

                return ResponseModel<Guid>.SuccessResponse(oldNote.Id, "تمت التعديل بنجاح");
            }
            catch (Exception ex)
            {
                return ResponseModel<Guid>.FailResponse($"{ex.Message}فشلت  التعديل   ");
            }
        }
    }
}
