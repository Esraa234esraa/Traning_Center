using TrainingCenterAPI.DTOs.Teacher.CLassesToTeacher;
using TrainingCenterAPI.DTOs.Teacher.ViewMyClasses;

namespace TrainingCenterAPI.Services.Teacher
{
    public interface ITeacherService
    {
        // CRUD
        Task<ResponseModel<List<GetAllTeacherDto>>> GetAllTeachersAsync();
        //Task<ResponseModel<TeacherWithClassesDto>> GetTeacherByIdAsync(Guid teacherId);
        Task<ResponseModel<Guid>> AddTeacherAsync(AddTeacherDto teacherDto);
        Task<ResponseModel<Guid>> AddClassToTeacherAsync(AddClassTeacherDto Dto);
        Task<ResponseModel<Guid>> UpdateTeacherAsync(Guid teacherId, UpdateTeacherDto teacherDto);
        Task<ResponseModel<bool>> DeleteTeacherAsync(Guid teacherId);
        //Task<ResponseModel<TeacherLoginResponse>> LoginTeacherAsync(string email, string password);

        Task<ResponseModel<TeacherViewDTO>> GetProfileTeacherWithClassesAsync(Guid teacherId);
        Task<ResponseModel<TeacherWithClassesDto>> GetTeacherWithClassesAsync(Guid teacherId);
        //Task<ResponseModel<AllStudentsForTeacherDto>> GetAllStudentsByTeacherIdAsync(Guid teacherId);
    }
}
