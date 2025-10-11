using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.Models.New;

namespace TrainingCenterAPI.Services.NewsServices
{
    public interface INewsService
    {
        Task<ResponseModel<List<GetAllCoursesDto>>> GetAllNewsAsync();

        Task<ResponseModel<List<GetAllCoursesDto>>> GetOnlyVisibleNewsAsync();

        Task<ResponseModel<News>> GetNewsByIdAsync(Guid id);
        Task<ResponseModel<Guid>> AddNewsAsync(AddCoursesDto courseDto);
        Task<ResponseModel<Guid>> UpdateNewsAsync(Guid id, PutCourseDto courseDto);
        Task<ResponseModel<bool>> DeleteNewsAsync(Guid id);
        Task<ResponseModel<bool>> HideNewsAsync(Guid id);
        Task<ResponseModel<bool>> VisibleNewsAsync(Guid id);
    }
}
