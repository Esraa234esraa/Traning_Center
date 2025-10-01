using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Models.ExternalCourses;

namespace TrainingCenterAPI.Services.ExternalCoursesServices
{
    public interface IExternalCoursesServices
    {
        Task<ResponseModel<List<GetAllCoursesDto>>> GetAllExternalCoursesAsync();



        Task<ResponseModel<ExternalCourse>> GetExternalCourseByIdAsync(Guid id);
        Task<ResponseModel<Guid>> AddExternalCourseAsync(AddCoursesDto courseDto);
        Task<ResponseModel<Guid>> UpdateExternalCourseAsync(Guid id, PutCourseDto courseDto);
        Task<ResponseModel<bool>> DeleteExternalCourseAsync(Guid id);

    }
}
