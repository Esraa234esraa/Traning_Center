using TrainingCenterAPI.DTOs.Student;

namespace TrainingCenterAPI.Services.Student
{
    public interface IStudentService
    {
        Task<ResponseModel<PostStudentDTO>> AddStudent(PostStudentDTO DTO);
        Task<ResponseModel<List<GetCurrentStudentDTO>>> GetCurrentStudentStudent();
    }
}
