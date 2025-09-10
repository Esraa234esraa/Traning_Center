using TrainingCenterAPI.DTOs.Teacher;
using TrainingCenterAPI.Responses;

namespace TrainingCenterAPI.Services.Interfaces
{
    public interface IClassService
    {
        Task<ResponseModel<ClassWithStudentsDto>> GetClassWithStudentsAsync(Guid classId);

        Task<ResponseModel<StudentDto>> AddStudentToClassAsync(Guid classId, Guid studentId, bool isPaid);
        Task<ResponseModel<StudentDto>> UpdateStudentInClassAsync(Guid classId, Guid studentId, bool isPaid);
        Task<ResponseModel<bool>> RemoveStudentFromClassAsync(Guid classId, Guid studentId);
    }

}
