using TrainingCenterAPI.DTOs.Classes;
using TrainingCenterAPI.DTOs.Courses;
using TrainingCenterAPI.DTOs.CurrentStudents;
using TrainingCenterAPI.DTOs.NewStudents;

namespace TrainingCenterAPI.Services.BasketServices
{
    public interface IBasketServices
    {
        Task<ResponseModel<List<GetAllCurrentStudentDTO>>> GetAllCurrentStudentDelete();
        Task<ResponseModel<List<GetAllNewStudentDTO>>> GetAllNewStudentDelete();
        Task<ResponseModel<List<GetAllNewStudentDTO>>> GetAllWaitingNewStudentDelete();
        Task<ResponseModel<List<GetAllTeacherDto>>> GetAllTeachersAsyncDelete();
        Task<ResponseModel<List<GetAllCoursesDto>>> GetAllCoursesAsyncDelete();
        Task<ResponseModel<List<GetAllClassesOfBouquetDTO>>> GetAllClassesDelete();
        Task<ResponseModel<bool>> DeleteTeacherAsyncDelete(Guid teacherId);
        Task<ResponseModel<string>> DeleteNewStudentDelete(Guid Id);
        Task<ResponseModel<string>> DeleteWaitingStudentDelete(Guid Id);
        Task<ResponseModel<bool>> DeleteCurrentStudentDelete(Guid Id);
        Task<ResponseModel<bool>> DeleteCourseAsyncDelete(Guid id);
        Task<ResponseModel<bool>> DeleteClassDelete(Guid Id);

    }
}
