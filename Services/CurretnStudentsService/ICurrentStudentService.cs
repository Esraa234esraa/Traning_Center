using TrainingCenterAPI.DTOs.CurrentStudents;

namespace TrainingCenterAPI.Services.CurretnStudentsService
{
    public interface ICurrentStudentService
    {
        Task<ResponseModel<Guid>> AddCurrentStudent(AddCurrentStudentDTO dTO);
        Task<ResponseModel<Guid>> UpdateCurrentStudent(Guid Id, UpdateCurrentStudentDTO dTO);
        Task<ResponseModel<bool>> DeleteCurrentStudent(Guid Id);
        Task<ResponseModel<ResponseDTO>> GetAllCurrentStudent(GetAllCurrentStudentQuery request);


    }
}
