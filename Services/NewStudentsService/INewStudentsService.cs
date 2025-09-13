using TrainingCenterAPI.DTOs.NewStudents;

namespace TrainingCenterAPI.Services.NewStudentsService
{
    public interface INewStudentsService
    {
        Task<ResponseModel<Guid>> AddNewStudent(PostNewStudentDTO DTO);

        Task<ResponseModel<List<GetAllNewStudentDTO>>> GetAllNewStudent();
        Task<ResponseModel<List<GetAllNewStudentDTO>>> GetAllWaitingNewStudent();
        Task<ResponseModel<Guid>> PutNewStudent(PutNewStudentDTO DTO, Guid Id);
        Task<ResponseModel<string>> DeleteNewStudent(Guid Id);
        Task<ResponseModel<string>> MoveNewStudentToWaitingStudent(Guid Id);

    }
}
