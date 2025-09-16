using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Models.Courses;

namespace TrainingCenterAPI.Services.CoursesServices
{
    public interface ICoursesServices
    {
        Task<ResponseModel<List<GetAllCoursesDto>>> GetAllCoursesAsync();

        Task<ResponseModel<Course>> GetCourseByIdAsync(Guid id);
        Task<ResponseModel<Guid>> AddCourseAsync(AddCoursesDto courseDto);
        Task<ResponseModel<Guid>> UpdateCourseAsync(Guid id, PutCourseDto courseDto);
        Task<ResponseModel<bool>> DeleteCourseAsync(Guid id);
        Task<ResponseModel<bool>> HideCourseAsync(Guid id);
        Task<ResponseModel<bool>> VisibleCourseAsync(Guid id);



    }
}
