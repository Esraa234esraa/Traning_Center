using TrainingCenterAPI.DTOs.Teacher;
using TrainingCenterAPI.Responses;


namespace TrainingCenterAPI.Services.Teacher
{
    public interface ITeacherService
    {
        // CRUD
        Task<ResponseModel<List<TeacherWithClassesDto>>> GetAllTeachersAsync();
        Task<ResponseModel<TeacherWithClassesDto>> GetTeacherByIdAsync(Guid teacherId);
        Task<ResponseModel<TeacherDto>> AddTeacherAsync(TeacherDto teacherDto, string password);
        Task<ResponseModel<TeacherDto>> UpdateTeacherAsync(Guid teacherId, TeacherDto teacherDto);
        Task<ResponseModel<bool>> DeleteTeacherAsync(Guid teacherId);
        Task<ResponseModel<TeacherLoginResponse>> LoginTeacherAsync(string email, string password);

        // Relations
        Task<ResponseModel<TeacherWithClassesDto>> GetTeacherWithClassesAsync(Guid teacherId);
        Task<ResponseModel<AllStudentsForTeacherDto>> GetAllStudentsByTeacherIdAsync(Guid teacherId);
    }
}
